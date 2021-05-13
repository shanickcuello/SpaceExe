using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public OpenGate gate;
    public Material aiMaterial;
    //public Light lightToChange;
    public List<Light> lightsToChange;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Device>() != null)
        {
            GetComponent<Renderer>().material = aiMaterial;
            ChangeLights();
            gate.TriggerOpen();
            //EventsManager.TriggerEvent("EVENT_OPENED");
        }
    }
    void ChangeLights()
    {
        foreach (Light light in lightsToChange)
        {
            if (light.enabled == false)
                light.enabled = true;
            light.color = aiMaterial.color;
        }
    }
}
