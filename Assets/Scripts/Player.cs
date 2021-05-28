using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using System;

public class Player : MonoBehaviour
{
    GameManager gameManagerSCR;
    [Header("Camera settings")]
    public Camera cameraPLayer;
    CameraTwo cameraScript;
    public GameObject cameraPivot;
    public float range;
    public float normalDistanceAvaivleToJump;
    public float currentDistanceAvaibleToJump;
    public float speedMovement;
    public float speedCameraMoovment;
    public float speedRotationCamera;
    public GameObject textPrefab;
    public Text myText;
    public List<Device> devicesNearMe;
    [SerializeField] FixedJoystick fixedJoystick;
    float _startTimeToMovement;
    float _distancetoTarget;

    [Header("Devices")]
    public bool isIndevice;
    public Device device;
    private bool isInRange;

    Vector3 _startPosition;
    Transform _myTransform;
    public Transform targetTransform;

    public GameObject gameManagerGO;

    [Header("Life")]
    //public Slider sliderBarLife;
    //public LifeBar lifeBar;
    public float life;

    MeshRenderer _playerMesh;

    [Header("Particles")]
    public ParticleSystem particlesSelection;

    [Header("Line Hover")]
    public LineRenderer line;

    [Header("[Testing]")]
    public GameObject directionalLight;
    [SerializeField] float simulationTime;


    void Start()
    {
        Time.timeScale = simulationTime;
        gameManagerSCR = gameManagerGO.GetComponent<GameManager>();
        _myTransform = transform;
        targetTransform = device.transform;
        SetInitialDestiny(targetTransform);

        life = 60;

        cameraPivot = GameObject.Find("CameraPivot");
        cameraScript = cameraPLayer.GetComponent<CameraTwo>();
        _playerMesh = transform.GetComponent<MeshRenderer>();

        RegisterCommands();
    }

    private void RegisterCommands()
    {
        Console.instance.RegisterCommand("maxlife", "Da vida máxima al player", MaxLife);
        Console.instance.RegisterCommand("superrange", "aumenta el rango de salto al infinito", SuperRange);
        Console.instance.RegisterCommand("superspeed", "aumenta la velocidad del jugador", SuperSpeed);
        Console.instance.RegisterCommand("light", "Enciende las luces de la escena", LightOn);
    }

    private void SuperSpeed()
    {
        speedMovement = 50;
    }

    void LightOn()
    {
        directionalLight.SetActive(true);
    }

    private void SuperRange()
    {
        range = 100;
        currentDistanceAvaibleToJump = 999;
        normalDistanceAvaivleToJump = 999;
    }

    private void MaxLife()
    {
        life = 999999;
    }

    void Update()
    {
        Move(targetTransform.position);
        MouseManager();
        LoseController();
        LifeManager();
        //ArrivedInDevice();

        if (isIndevice)
            OnDeviceStay();

        gameManagerSCR.lifeBar.fillValue = life * 1 / 60;
    }

    public void MouseManager()
    {
        Raycast();
    }


    public void Raycast()
    {
        RaycastHit hitInfo;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, range))
        {
            if (hitInfo.transform.gameObject.GetComponent<Device>() == true && currentDistanceAvaibleToJump > Vector3.Distance(transform.position, hitInfo.transform.position))
            {  
                var hitDevice = hitInfo.transform.gameObject.GetComponent<Device>();
                textPrefab.SetActive(true);
                myText.text = DeviceManager.GetObjectiveNameAndDistance(transform.position, devicesNearMe, hitDevice);
                
                if (hitInfo.transform.gameObject.GetComponent<Device>())
                {
                    Device deviceToGo = hitInfo.transform.gameObject.GetComponent<Device>();
                    if (hitInfo.transform.gameObject.GetComponent<Cuchurruchin>())
                    {
                        fixedJoystick.gameObject.SetActive(true);
                    }
                    else
                    {
                        fixedJoystick.gameObject.SetActive(false);
                    }

                    if (!deviceToGo.canjump)
                    {
                        return;
                    }
                }

                if (line.enabled == false && isIndevice)
                {
                    line.enabled = true;
                }

                line.SetPosition(0, transform.position);
                line.SetPosition(1, hitInfo.transform.position);

                if (Input.GetButtonDown("Fire1"))
                {
                    line.enabled = false;
                    cameraScript.localIifirstPerson = false;
                    var newParticleInstance = Instantiate(particlesSelection);
                    newParticleInstance.transform.SetPositionAndRotation(hitInfo.transform.position, Quaternion.identity);
                    SetNewDestiny(hitInfo.transform);
                    GoalsManager.instance.playerAmountOfJumps++;
                }
            }
            else
            {
                if (line.enabled == true)
                {
                    line.enabled = false;
                    textPrefab.SetActive(false);
                }
            }
        }
    }

    public void Move(Vector3 target)
    {
        if (transform.position != target && _distancetoTarget <= currentDistanceAvaibleToJump)
        {
            //isIndevice = false;
            //_device.playerIsHere = false;
            float _currentDistance = (Time.time - _startTimeToMovement) * speedMovement;
            float _fracJourney = _currentDistance / _distancetoTarget;
            transform.position = Vector3.Lerp(_startPosition, target, _fracJourney);

            if (_fracJourney >= 1)
            {
                //isIndevice = true;
                OnDeviceEnter();
                currentDistanceAvaibleToJump = normalDistanceAvaivleToJump;
            }

        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        gameManagerSCR.BeingAttacked();
        GoalsManager.instance.playerLooseLife = true;
    }

    public void LoseController()
    {
        if (life <= 0)
        {
            gameManagerSCR.Lose(true);

        }
    }

    public void LifeManager()
    {
        if (life >= 60)
        {
            life = 60;
        }
    }

    public void LifeController()
    {
        life -= Time.deltaTime * 10;
        gameManagerSCR.BeingAttacked();
    }

    public void ForceSetNewDestiny(Transform _trgtTr)
    {
        OnDeviceExit();
        targetTransform = _trgtTr;
        _distancetoTarget = Vector3.Distance(transform.position, targetTransform.position);
        _startTimeToMovement = Time.time;
        _startPosition = _myTransform.position;
    }

    void OnDeviceEnter()
    {
        isIndevice = true;
        devicesNearMe = DeviceManager.GetNearMe(targetTransform.position, normalDistanceAvaivleToJump);
        //_device.SetMaterial(_playerMesh.material);
        device = targetTransform.GetComponent<Device>();

        if (isIndevice)
        {
            device.HackDevice();
        }

        device.OnEntityEnter(0);
        _playerMesh.enabled = false;

        //Nuevo
        //GetComponentInChildren<VisualEffect>().Stop();
    }
    void OnDeviceStay()
    {
        device.CheckKeys();
    }
    void OnDeviceExit()
    {
        isIndevice = false;
        device.OnEntityExit();
        _playerMesh.enabled = true;

        //GetComponentInChildren<VisualEffect>().Play();
    }

    void SetNewDestiny(Transform _trgtTr)
    {
        OnDeviceExit();
        targetTransform = _trgtTr;
        _distancetoTarget = Vector3.Distance(transform.position, targetTransform.position);
        _startTimeToMovement = Time.time;
        _startPosition = _myTransform.position;
        GetComponent<AudioSource>().PlayOneShot(SFXPlayer.instance.GetSound("s_shoot_rifle1"));
    }

    void SetInitialDestiny(Transform _trgtTr)
    {
        targetTransform = _trgtTr;
        _distancetoTarget = Vector3.Distance(transform.position, targetTransform.position);
        _startTimeToMovement = Time.time;
        _startPosition = _myTransform.position;
        device = targetTransform.GetComponent<Device>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<CheckPoint>() != null)
        {
            other.gameObject.GetComponent<CheckPoint>().SetCheckPoint();
        }
    }
}
