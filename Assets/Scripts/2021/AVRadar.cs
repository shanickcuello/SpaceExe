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
        base.Update();
        DoScanAction();
        ScanForPlayer();
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
                Debug.Log("vialplayer");
            }

            else
            {
                seenPlayer = false;
                Debug.Log("novialplayer");
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
