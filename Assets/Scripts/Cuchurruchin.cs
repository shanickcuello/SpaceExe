using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuchurruchin : Device
{
    public float speedMovement;
    CameraTwo cameraSCR;
    Vector3 directions;
    Rigidbody rb;
    public FixedJoystick fixedJoystick;
    float hMove, vMove;

    protected override void Start()
    {
        base.Start();
        directions = new Vector3(0, 0, 0);
        cameraSCR = Camera.main.GetComponent<CameraTwo>();
        rb = gameObject.GetComponent<Rigidbody>();
        isShooteable = true;
    }

    protected override void Update()
    {
        base.Update();
        CheckPlayer();
    }

    void CheckPlayer()
    {
        if (playerIsHere)
        {
            rb.isKinematic = false;
            fixedJoystick.gameObject.SetActive(true);
        }
        else if (!playerIsHere)
        {
            rb.isKinematic = true;
        }

    }

    public void Move()
    {
        GameObject cameraGO = Camera.main.gameObject;
        if (cameraSCR.currCamIndex == 1)
        {
            float axisOne = Input.GetAxisRaw("Vertical");
            float axisTwo = Input.GetAxisRaw("Horizontal");
            hMove = fixedJoystick.Horizontal * 3;
            vMove = fixedJoystick.Vertical * 3;
            rb.velocity = new Vector3(-axisOne, 0, axisTwo).normalized * speedMovement;
            rb.velocity = new Vector3(-vMove, 0, hMove).normalized * speedMovement;
        }
        else if (cameraSCR.currCamIndex == 0)
        {
            hMove = fixedJoystick.Horizontal * 3;
            vMove = fixedJoystick.Vertical * 3;

            float axisOne = Input.GetAxisRaw("Horizontal");
            float axisTwo = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(axisOne, 0, axisTwo).normalized * speedMovement;

            rb.velocity = new Vector3(hMove, 0, vMove).normalized * speedMovement;


        }
        else if (cameraSCR.currCamIndex == 2)
        {
            hMove = fixedJoystick.Horizontal * 3;
            vMove = fixedJoystick.Vertical * 3;


            float axisOne = Input.GetAxisRaw("Horizontal");
            float axisTwo = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector3(-axisOne, 0, -axisTwo).normalized * speedMovement;

            rb.velocity = new Vector3(-hMove, 0, -vMove).normalized * speedMovement;
        }
        else if (cameraSCR.currCamIndex == 3)
        {
            hMove = fixedJoystick.Horizontal * 3;
            vMove = fixedJoystick.Vertical * 3;
            float axisOne = Input.GetAxisRaw("Vertical");
            float axisTwo = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector3(axisOne, 0, -axisTwo).normalized * speedMovement;
            rb.velocity = new Vector3(vMove, 0, -hMove).normalized * speedMovement;
        }

    }

    public override void CheckKeys()
    {
        Move();
    }

}
