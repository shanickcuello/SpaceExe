using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoxDevice : Device
{
    public float howMuchCure;

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            Player playerSCR = player.GetComponent<Player>();
            playerSCR.life += howMuchCure * Time.deltaTime;
        }
    }
}
