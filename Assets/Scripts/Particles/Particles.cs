using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public float destroyInSeconds;
    float _currTime = 0;

    private void Update()
    {

        if(_currTime > destroyInSeconds)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _currTime += Time.deltaTime;
        }
    }
}
