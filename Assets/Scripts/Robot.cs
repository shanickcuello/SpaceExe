using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Robot : Device
{
    //Public variables
    public NavMeshAgent agentIA;
    public List<GameObject> wayPoints = new List<GameObject>();
    public bool torretMode, watchOver, attackPlayer, moveToPlayer, countTime, shoot;
    public int indexWayPoint;
    public GameObject playerGO, bullet;
    public float currentTime, timeToStopAttack, shootSpeed, speedMovement;
    CameraTwo cameraSCR;
    Rigidbody rb;
    Vector3 direction;

    //Private Variables
    Player playerSCR;
    Light light;

    private void Awake()
    {
        shoot = true;
        torretMode = false;
        agentIA = GetComponent<NavMeshAgent>();
        playerSCR = FindObjectOfType<Player>();
        playerGO = playerSCR.gameObject;
        light = GetComponentInChildren<Light>();

    }

    protected override void Update()
    {
        base.Update();
        WatchOver();
        CheckKeys();
        AttackPlayer();
        MoveToPlayer();
        CheckTime();
        PlayerIsHere();
    }

    protected override void Start()
    {
        base.Start();
        cameraSCR = Camera.main.GetComponent<CameraTwo>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    protected override void ChangeMaterial(Material material)
    {
        return;
    }

    public void RotateToPlayer()
    {

    }

    public void AttackPlayer()
    {
        if (attackPlayer)
        {

            watchOver = false;
        }
    }

    public void MoveToPlayer()
    {

        if (moveToPlayer && Vector3.Distance(transform.position, playerGO.transform.position) > 6)
        {
            watchOver = false;
            canjump = false;
            transform.LookAt(playerGO.transform);
            // NO ATACA AL PLAYER
            light.color = Color.yellow;
            agentIA.isStopped = false;
            agentIA.SetDestination(playerGO.transform.position);
            countTime = true;
        }
        else if (moveToPlayer && Vector3.Distance(transform.position, playerGO.transform.position) <= 6)
        {
            canjump = false;
            watchOver = false;

            //  ATACA EL PLAYER
            Shoot();
            agentIA.isStopped = true;
            light.color = Color.red;
            countTime = false;
        }

    }


    public void Shoot()
    {
        if (shoot)
        {
            //transform.LookAt(playerGO.transform);
            transform.forward = player.transform.position - transform.position;
            Instantiate(bullet, transform.position, transform.rotation);
            bullet.layer = this.gameObject.layer;
            shoot = false;
            Invoke("AvaibleToShoot", shootSpeed);
        }
    }

    public void AvaibleToShoot()
    {
        shoot = true;
    }

    public void WatchOver()
    {

        Transform target = wayPoints[indexWayPoint].transform;
        if (watchOver)
        {
            canjump = true;
            agentIA.isStopped = false;
            light.color = Color.white;
            agentIA.SetDestination(target.position);
            if (Vector3.Distance(transform.position, target.position) < 1)
            {
                if (indexWayPoint < wayPoints.Count - 1)
                {
                    indexWayPoint += 1;
                }
                else
                {
                    indexWayPoint = 0;
                }
            }
        }
    }

    public override void CheckKeys()
    {
        if (playerIsHere)
        {
            agentIA.isStopped = true;
            Move();

        }
    }

    void Move()
    {

        GameObject cameraGO = Camera.main.gameObject;
        if (cameraSCR.currCamIndex == 1)
        {
            float axisOne = Input.GetAxisRaw("Vertical");
            float axisTwo = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(-axisOne, 0, axisTwo).normalized * speedMovement;

        }
        else if (cameraSCR.currCamIndex == 0)
        {
            float axisOne = Input.GetAxisRaw("Horizontal");
            float axisTwo = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(axisOne, 0, axisTwo).normalized * speedMovement;
        }
        else if (cameraSCR.currCamIndex == 2)
        {
            float axisOne = Input.GetAxisRaw("Horizontal");
            float axisTwo = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(-axisOne, 0, -axisTwo).normalized * speedMovement;
        }
        else if (cameraSCR.currCamIndex == 3)
        {
            float axisOne = Input.GetAxisRaw("Vertical");
            float axisTwo = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(axisOne, 0, -axisTwo).normalized * speedMovement;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position, transform.rotation);
            bullet.layer = this.gameObject.layer;
        }*/
    }

    public void CheckTime()
    {

        if (countTime)
            currentTime += Time.deltaTime;
        else
            currentTime = 0;

        if (currentTime >= timeToStopAttack)
        {
            agentIA.isStopped = false;
            attackPlayer = false;
            moveToPlayer = false;
            watchOver = true;
        }
    }

    public void PlayerIsHere()
    {
        if (!playerIsHere)
        {
            agentIA.isStopped = false;
            rb.isKinematic = true;

        }
        else
        {
            rb.isKinematic = false;

            Move();
            agentIA.isStopped = true;
            attackPlayer = false;
            moveToPlayer = false;
            watchOver = false;
        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Device>())
        {
            Device device = other.GetComponent<Device>();
            if (device.playerIsHere && device.isShooteable)
            {
                watchOver = false;
                attackPlayer = true;
                moveToPlayer = true;
                countTime = false;
            }
        }

    }


}
