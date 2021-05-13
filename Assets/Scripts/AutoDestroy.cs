using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    private void Awake()
    {
        Invoke("Destroy", 1);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
