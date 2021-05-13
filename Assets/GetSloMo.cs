using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSloMo : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ActiveSloMo();
    }

    private void ActiveSloMo()
    {
        if (Vector3.Distance(transform.position, player.transform.position)<1)
        {
            player.transform.GetComponent<SlowMo>().activeSlowMo = true;
        }
    }
}
