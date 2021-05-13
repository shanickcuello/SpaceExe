using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public OpenHoldGate holdingGate;
    public Material aiMaterial;
    public Light lightToChange;
    public List<Light> lightsToChange;
    Cuchurruchin[] cuchurrumins;
    Material _selfMaterial;
    bool openGate;

    private void Awake()
    {
        _selfMaterial = GetComponent<Renderer>().material;
        cuchurrumins = FindObjectsOfType<Cuchurruchin>();
    }

    private void Update()
    {
        CheckDistanceToCuchurruminToOpenDoor();
        OpenGate();
    }

    private void OpenGate()
    {
        if (openGate)
        {
            GetComponent<Renderer>().material = aiMaterial;
            //ChangeLightColor(aiMaterial.color);
            EnableLights();
            holdingGate.EnableHoldOpen();
        }
        else
        {
            GetComponent<Renderer>().material = _selfMaterial;
            //ChangeLightColor(_selfMaterial.color);
            DisableLights();
            holdingGate.DisableHoldOpen();
        }
    }

    void CheckDistanceToCuchurruminToOpenDoor()
    {
        foreach (var item in cuchurrumins)
        {
            if (Vector3.Distance(transform.position, item.transform.position) < 1)
            {
                openGate = true;
                break;
            }
            else
            {
                openGate = false;
            }
        }
        
    }

    void EnableLights()
    {
        foreach (Light light in lightsToChange)
        {
            if (light.enabled == false)
                light.enabled = true;
            light.color = aiMaterial.color;
        }
    }
    void DisableLights()
    {
        foreach (Light light in lightsToChange)
        {
            if (light != lightToChange && light.enabled == true)
                light.enabled = false;
            else
                light.color = _selfMaterial.color;
        }
    }
}
