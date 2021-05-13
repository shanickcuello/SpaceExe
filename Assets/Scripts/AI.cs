using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    protected int defaultDestinyIndex;

    public float speedMovement;
    public float timeInDevice;

    protected float distanceToDevice;
    protected float startTime;
    protected float currentTimeInDevice;

    protected Vector3 startPosition;
    protected Vector3 targetPosition;

    public List<Device> devices = new List<Device>();

    protected bool arrivedToDevice;
    

    void Start()
    {
        defaultDestinyIndex = 0;
    }

    virtual protected void SetNewDestiny()
    {
        targetPosition = devices[defaultDestinyIndex].transform.position;
        startTime = Time.time;
        startPosition = transform.position;
        distanceToDevice = Vector3.Distance(startPosition, targetPosition);
        arrivedToDevice = false;
    }




}
