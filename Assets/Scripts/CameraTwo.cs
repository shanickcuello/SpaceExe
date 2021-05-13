using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwo : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool localIifirstPerson;

    public List<Vector3> cameraPos = new List<Vector3>();
    Vector3 currPos;
    public int currCamIndex = 1;

    Player playerSCR;

    //ToachInputs
    Vector3 startToachPosition, endToachPosition, currentToachPosition;
    bool toaching;

    void Awake()
    {
        smoothSpeed = 1;
        currPos = cameraPos[currCamIndex];
    }

    private void Start()
    {

        playerSCR = player.GetComponent<Player>();
    }

    private void Update()
    {
        Smooth();
        CheckInputs();
    }

    void CheckInputs()
    {
        offset = currPos;

        if (!localIifirstPerson)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateCamera(RotateCameraDirection.right);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateCamera(RotateCameraDirection.left);
            }

            //Debug.Log(Vector3.Distance(startToachPosition, currentToachPosition));
        }
    }

    public void RotateCamera(int dir)
    {
        if (dir == 1)
        {
            RotateCamera(RotateCameraDirection.left);
        }
        else
        {
            RotateCamera(RotateCameraDirection.right);
        }
    }

    Vector3 GetToachPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void RotateCamera(RotateCameraDirection dir)
    {
        if (dir == RotateCameraDirection.left)
        {
            smoothSpeed = 0.3f;

            if (currCamIndex <= 0)
            {
                currCamIndex = cameraPos.Count - 1;
            }
            else
            {
                currCamIndex--;
            }

            currPos = cameraPos[currCamIndex];
            Invoke("SetCameraSpeed", 0.3f);
        }
        else if (dir == RotateCameraDirection.right)
        {
            smoothSpeed = 0.3f;
            if (currCamIndex < cameraPos.Count - 1)
            {
                currCamIndex++;
            }
            else
            {
                currCamIndex = 0;
            }
            currPos = cameraPos[currCamIndex];
            Invoke("SetCameraSpeed", 0.3f);
        }
    }

    void Smooth()
    {
        if (player && !localIifirstPerson)
        {
            Vector3 posicionDeseada = player.position + offset;
            Vector3 posicionSmooth = Vector3.Lerp(transform.position, posicionDeseada, smoothSpeed);
            transform.position = posicionSmooth;
            transform.LookAt(player);

        }

    }

    public void SetCameraPosition(Vector3 cameraPos, Vector3 cameraPosForward, bool isFirstPerson)
    {
        if (isFirstPerson)
        {

            localIifirstPerson = true;
            transform.position = cameraPos;
            transform.forward = cameraPosForward;

        }

    }

    public void SetCameraSpeed()
    {
        smoothSpeed = 1;
    }

}

public enum RotateCameraDirection
{
    right,
    left
}
