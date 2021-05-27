using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class AV : MonoBehaviour
{
    [Header("Basic Settigns")]
    public float speed;
    public float timeInDevice;

    [Header("Line Path Settings")]
    public LineRenderer linePrefab;
    LineRenderer _line;

    public int destinyIndex = 0;
    float _journeyLength;
    float _startTime;
    float _currTimeInDevice;
    Vector3 _startPosition;
    Vector3 _targetPosition;

    [Header("Device pattern")]
    public List<Device> devices = new List<Device>();
    Device _currDevice;

    MeshRenderer _renderer;

    //esto no esta en herencia
    public Player playerSCR;


    public bool _arrived = false;

    //no esta en herencia
    public GameObject playerGO;

    protected virtual void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        _line.enabled = false;

    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
      
        playerGO = FindObjectOfType<Player>().gameObject;
        playerSCR = playerGO.GetComponent<Player>();
        SettingNewDestiny();
       
    }

    protected virtual void Update()
    {

        if ((transform.position == _targetPosition) && !_arrived)
            ArrivedMethod();

        if (!_arrived)
            Movement();

        if (_arrived)
        {
            _currTimeInDevice -= Time.deltaTime;

            //Si al jugador le falta la mitad de tiempo para salir del dispositivo actual, muestra la línea apuntando al próximo dispositivo a saltar.
            if (_currTimeInDevice <= (timeInDevice / 2) && !_line.enabled)
            {
                int _tempDestIndex = destinyIndex + 1;
                if (_tempDestIndex >= devices.Count)
                {
                    _tempDestIndex = 0;
                }

                _line.enabled = true;
                _line.SetPosition(0, transform.position);
                _line.SetPosition(1, devices[_tempDestIndex].transform.position);
            }

            if (_currTimeInDevice <= 0)
            {

                _line.enabled = false;
                //_currDevice.SetDefaultMaterial();
                _currDevice.OnEntityExit();

                destinyIndex++;
                if (destinyIndex >= devices.Count)
                {
                    destinyIndex = 0;
                }

                SettingNewDestiny();
            }
        }

        Attack();
        
    }


    void Attack()
    {
        if (transform.position == playerGO.transform.position)
        {
            playerSCR.TakeDamage();
        }


    }

    void Movement()
    {
        _targetPosition = devices[destinyIndex].transform.position;
        float _distCovered = (Time.time - _startTime) * speed;
        float _fracJourney = _distCovered / _journeyLength;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _fracJourney);
    }

    void ArrivedMethod()
    {
        _arrived = true;
        _currTimeInDevice = timeInDevice;

        _renderer.enabled = false;
        //GetComponentInChildren<VisualEffect>().Stop();

        SetMaterialToDevice();
    }

    protected virtual void SettingNewDestiny()
    {
        _targetPosition = devices[destinyIndex].transform.position;
        _startTime = Time.time;
        _startPosition = transform.position;
        _journeyLength = Vector3.Distance(_startPosition, _targetPosition);

        _arrived = false;
        //GetComponentInChildren<VisualEffect>().Play();
        _renderer.enabled = true;
    }

    void SetMaterialToDevice()
    {
        _currDevice = devices[destinyIndex];
        //_currDevice.SetMaterial(GetComponent<Renderer>().material);
        _currDevice.OnEntityEnter(1);
    }
}
