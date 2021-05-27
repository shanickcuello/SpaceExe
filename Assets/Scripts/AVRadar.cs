using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AVRadar : AV
{
    Device deviceTarget, newTarget;
    [SerializeField] Device startDevice, endDevice;
    float currentCounter;
    [SerializeField] float counterToAttack;
    bool followPlayer;
    public float distanceToScan;
    public bool seenPlayer;
    public bool scanned;
    public float scanCounter;
    public Device targetToAdd;
    public float scanTime;
    public Device targetDevice;

    protected override void Update()
    {

        if ((transform.position == _targetPosition) && !_arrived)
        {
            DoScanAction();
            ArrivedMethod();
        }
        
        if (!_arrived)
            Movement();

        if (_arrived)
        {
            _currTimeInDevice -= Time.deltaTime;

            //Si al jugador le falta la mitad de tiempo para salir del dispositivo actual, muestra la línea apuntando al próximo dispositivo a saltar.
            if (_currTimeInDevice <= (timeInDevice / 2) && !_line.enabled)
            {
                int _tempDestIndex = 0;

                if (!seenPlayer)
                {
                    
                    
                    if (_tempDestIndex >= devices.Count)
                    {
                        _tempDestIndex = 0;
                    }
                }
                else _tempDestIndex = destinyIndex;
                
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

    protected override void Start()
    {
        devices = DeviceManager.GetNearMe(transform.position, distanceToScan);
        base.Start();
        
    }

    void DoScanAction()
    {
        if (_arrived)
        {
            scanCounter -= Time.deltaTime;
            if (scanCounter <= 0 && !scanned)
            {
                ScanForPlayer();
                scanned = true;
                scanCounter = scanTime;
                Debug.Log("escaneo");
            }
        }
        else
        {
            scanned = false;
            
            if (scanCounter != scanTime)
                scanCounter = scanTime;
        }

    }

    void ScanForPlayer()
    {

        if (!scanned)
        {
            var position = transform.position;
        
            var scanDevices = DeviceManager.GetNearMe(position, distanceToScan);
        
            var distanceToPlayer = Vector3.Distance(position, playerSCR.transform.position);

            if (distanceToPlayer <= distanceToScan)
            {
                seenPlayer = true;
            }

            else
            {
                seenPlayer = false;
            }

            if (seenPlayer)
            {
                targetToAdd = scanDevices.SkipWhile(x => x.playerIsHere == false)
                    .Take(1)
                    .FirstOrDefault();

                if (targetToAdd != null)
                {
                    targetDevice = targetToAdd;
                
                    if (!devices.Contains(targetToAdd))
                    {
                        devices.Add(targetToAdd); 
                    }

                    destinyIndex = devices.IndexOf(targetToAdd);
                    
                }
                seenPlayer = false;

            }
        }

    }
    
}
