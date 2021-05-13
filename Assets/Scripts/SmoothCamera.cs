using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{

    public Transform player;
    public float velocidadSualizado = 0.125f;
    public Vector3 offset;
    public bool localIifirstPerson;

    public List<Vector3> cameraPos = new List<Vector3>();
    Vector3 currPos;
    public int currCamIndex = 1;

    Player playerSCR;
    

    void Awake()
    {

        currPos = cameraPos[currCamIndex];
    }

    private void Start()
    {

        playerSCR = player.GetComponent<Player>();
    }

    private void Update()
    {
        Smooth();
        ChangeCameraPosition();
    }

    void ChangeCameraPosition()
    {
        offset = currPos;
        if (!localIifirstPerson)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currCamIndex < cameraPos.Count - 1)
                {
                    currCamIndex++;
                }
                else
                {
                    currCamIndex = 0;
                }
                currPos = cameraPos[currCamIndex];
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currCamIndex <= 0)
                {
                    currCamIndex = cameraPos.Count - 1;
                }
                else
                {
                    currCamIndex--;
                }

                currPos = cameraPos[currCamIndex];
            }
        }


    }
    
    void Smooth()
    {
        if(player && !localIifirstPerson)
        {
            Vector3 posicionDeseada = player.position + offset;
            Vector3 posicionSmooth = Vector3.Lerp(transform.position, posicionDeseada, velocidadSualizado);
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


}
