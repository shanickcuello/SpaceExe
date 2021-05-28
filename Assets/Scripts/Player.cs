using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using System;

public class Player : MonoBehaviour, IGridEntity
{
    GameManager gameManagerSCR;
    [Header("Camera settings")]
    public Camera cameraPLayer;
    CameraTwo cameraScript;
    public GameObject cameraPivot;
    public float jumpRange;
    public float normalDistanceAvaivleToJump;
    public float currentDistanceAvaibleToJump;
    public float speedMovement;
    public float speedCameraMoovment;
    public float speedRotationCamera;
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

    [Header("Controller")]
    [SerializeField] KeyCode jumpKeyCode;

    MeshRenderer _playerMesh;

    [Header("Particles")]
    public ParticleSystem particlesSelection;

    [Header("Line Hover")]
    public LineRenderer line;

    [Header("[Testing]")]
    public GameObject directionalLight;
    [SerializeField] float simulationTime;

    public Queue<Device> visitedsDevices = new Queue<Device>();

    public event Action<IGridEntity> OnMove;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }


    void Start()
    {
        Time.timeScale = simulationTime;
        gameManagerSCR = gameManagerGO.GetComponent<GameManager>();
        _myTransform = transform;
        targetTransform = device.transform;
        SetInitialDestiny(targetTransform);
        InitialiceVisitedsDecivesQueue();

        life = 100;

        cameraPivot = GameObject.Find("CameraPivot");
        cameraScript = cameraPLayer.GetComponent<CameraTwo>();
        _playerMesh = transform.GetComponent<MeshRenderer>();

        RegisterCommands();
    }

    private void InitialiceVisitedsDecivesQueue()
    {
        for (int i = 0; i < 20; i++)
        {
            Jump();
        }
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
        jumpRange = 100;
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
        CheckInputs();
        //ArrivedInDevice();

        if (isIndevice)
            OnDeviceStay();

        gameManagerSCR.lifeBar.fillValue = life * 1 / 100;
    }

    private void CheckInputs()
    {
        if (Input.GetKeyUp(jumpKeyCode))
        {
            Jump();
        }
    }


    void Jump()
    {

        Device deviceToJump = GetNearestDevice(); //IA2-P3  Chequear el codigo dentro de DeviceManager

        if (deviceToJump.transform.gameObject.GetComponent<Device>() == true && currentDistanceAvaibleToJump > Vector3.Distance(transform.position, deviceToJump.transform.position))
        {

            Device deviceToGo = deviceToJump.transform.gameObject.GetComponent<Device>();
            if (deviceToJump.transform.gameObject.GetComponent<Cuchurruchin>())
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


            if (line.enabled == false && isIndevice)
            {
                line.enabled = true;
            }

            line.SetPosition(0, transform.position);
            line.SetPosition(1, deviceToJump.transform.position);
            line.enabled = false;
            cameraScript.localIifirstPerson = false;
            var newParticleInstance = Instantiate(particlesSelection);
            newParticleInstance.transform.SetPositionAndRotation(deviceToJump.transform.position, Quaternion.identity);
            SetNewDestiny(deviceToJump.transform);
            GoalsManager.instance.playerAmountOfJumps++;
        }
        else
        {
            if (line.enabled == true)
            {
                line.enabled = false;
            }
        }
    }

    Device GetNearestDevice()
    {
        Device nearestDevice = DeviceManager.GetNearestDevice(transform.position, 50, visitedsDevices);
        visitedsDevices.Enqueue(nearestDevice);

        if (visitedsDevices.Count > 20)
        {
            visitedsDevices.Dequeue();
        }
        return visitedsDevices.Peek();
    }

    public void MouseManager()
    {
        Raycast();
    }


    public void Raycast()
    {
        RaycastHit hitInfo;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, jumpRange))
        {
            if (hitInfo.transform.gameObject.GetComponent<Device>() == true && currentDistanceAvaibleToJump > Vector3.Distance(transform.position, hitInfo.transform.position))
            {

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
                }
            }
        }
    }

    public void Move(Vector3 target)
    {
        if (transform.position != target && _distancetoTarget <= currentDistanceAvaibleToJump)
        {

            float _currentDistance = (Time.time - _startTimeToMovement) * speedMovement;
            float _fracJourney = _currentDistance / _distancetoTarget;
            transform.position = Vector3.Lerp(_startPosition, target, _fracJourney);
            OnMove(this);
            if (_fracJourney >= 1)
            {
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
        if (life >= 100)
        {
            life = 100;
        }
    }

    public void TakeDamage()
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
