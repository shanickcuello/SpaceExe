using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AVRadar : AV
{
    [SerializeField] Device startDevice, endDevice;
    [SerializeField] float counterToAttack;
    public float distanceToScan;
    public bool seenPlayer;
    public bool scanned;
    public float scanCounter;
    public Device targetToAdd;
    public float scanTime;
    public Device targetDevice;
    public Device nearest;
    float currentCounter;
    Device deviceTarget, newTarget;
    bool followPlayer;

    protected override void Start()
    {
        devices = DeviceManager.GetNearMe(transform.position, distanceToScan);
        devices.Add(DeviceManager.GetNearestDevice(transform.position,distanceToScan));
        nearest = DeviceManager.GetNearestDevice(transform.position, distanceToScan);
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        DoScanAction();
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
