using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    bool active;
    [SerializeField] GameObject joystick;
    Cuchurruchin currentCuchurrumin;

    private void Start()
    {
        joystick.SetActive(false);
    }

    public void SetActivePanel(bool value, Cuchurruchin cuchurrimin)
    {
        if (value == true && !active)
        {
            currentCuchurrumin = cuchurrimin;
            active = true;
            joystick.SetActive(value);
        }
        else if (value == false && active)
        {
            active = false;
            joystick.SetActive(value);
        }
    }

    public void SetActivePanel(bool value)
    {
        if (value == true && !active)
        {
            active = true;
            joystick.SetActive(value);
        }
        else if (value == false && active)
        {
            active = false;
            joystick.SetActive(value);
        }
    }

}
