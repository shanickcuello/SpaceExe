using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackPanel : MonoBehaviour
{
    //public List<GameObject> ductsList = new List<GameObject>();
    public List<HackDuct> ductsList = new List<HackDuct>();
    public GameObject hackPanel;
    public float currentPointsHacked, pointsToHacked;
    public bool allAreTrue;

    public OpenGate gateToOpen;

    void Update()
    {
        TravelList();
    }

    void TravelList()
    {
        int trueCounts = 0;

        foreach (HackDuct duct in ductsList)
        {
            if(duct.unlock)
            {
                trueCounts++;
            }
        }

        if(trueCounts == ductsList.Count)
        {
            allAreTrue = true;
            TriggerEvent();
        }
    }
    void TriggerEvent()
    {
        gateToOpen.TriggerOpen();
        Reset();
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        allAreTrue = false;
    }
}
