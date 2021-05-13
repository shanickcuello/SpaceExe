using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HackDuct : MonoBehaviour, IPointerClickHandler
{
    HackPanel hackpanelSCR;
    public bool unlock;

    void Update()
    {


    }

    private void Rotate()
    {
        transform.Rotate(transform.forward, 90);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HackDuct>())
        {
            unlock = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HackDuct>())
        {
            unlock = false;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Rotate();
    }
}
