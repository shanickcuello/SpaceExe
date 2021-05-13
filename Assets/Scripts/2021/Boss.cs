using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : AV
{
    Device deviceTarget, newTarget;
    [SerializeField] Device startDevice, endDevice;
    float currentCounter;
    [SerializeField] float counterToAttack;
    bool followPlayer;

    protected override void Update()
    {
        base.Update();

        FollowAndAttackPlayer();
    }

    void FollowAndAttackPlayer()
    {
        currentCounter += Time.deltaTime;

        if (currentCounter > counterToAttack)
        {
            followPlayer = true;
        }

        if (GetCurrentDeviceOfPlayer() == endDevice)
        {
            return;
        }

        if (devices[devices.Count - 1] != GetCurrentDeviceOfPlayer() && followPlayer)
        {
            devices.Clear();
            devices.Add(GetCurrentDeviceOfPlayer());
        }
    }

    Device GetCurrentDeviceOfPlayer()
    {
        if (playerSCR.device != null)
        {
            return playerSCR.device;
        }
        else
        {
            return startDevice;
        }
    }

}
