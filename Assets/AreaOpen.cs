using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOpen : MonoBehaviour
{
    OpenGate openGate;
    GameObject player;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 2)
        {
            openGate.TriggerOpen();
        }
        else
        {
            openGate.TriggerClose();
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        openGate = GetComponent<OpenGate>();
    }
    
}
