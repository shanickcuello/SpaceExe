using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDevice : Device
{

    CameraTwo _cameraSCR;
    GameObject cameraGO;
    public float rangeOfJumpToPlayer;
    bool firstPerson;
    Player playerSCR;
    

    protected override void Start()
    {
        base.Start();
        cameraGO = Camera.main.gameObject;
        _cameraSCR = cameraGO.GetComponent<CameraTwo>();
        playerSCR = player.GetComponent<Player>();
    }
    
    public override void CheckKeys()
    {

        if (Input.GetMouseButtonUp(1))
        {
            if (!firstPerson)
            {
                _cameraSCR.SetCameraPosition(transform.position, transform.forward, true);
                playerSCR.currentDistanceAvaibleToJump = 10000;
                firstPerson = true;
            }
            else if (firstPerson && Input.GetMouseButtonUp(1))
            {
                _cameraSCR.localIifirstPerson = false;
                playerSCR.currentDistanceAvaibleToJump = playerSCR.normalDistanceAvaivleToJump;

                firstPerson = false;

            }
        }
    }

    override public void OnEntityExit() { }
    override public void OnEntityEnter(int selection) { }
}
