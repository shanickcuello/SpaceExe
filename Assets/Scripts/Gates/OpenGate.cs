using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [Header("Control por animator")]
    public bool controledByAnimator = false;
    public Animator anim;
    Animator _anim;

    [Header("Control propio")]
    public float time;
    public float speedShift;

    bool _opening;
    float _currTime;

    private void Awake()
    {
        if (controledByAnimator)
        {
            if (anim != null)
            {
                _anim = anim;
            }
            else if (TryGetComponent(out anim))
            {
                _anim = anim;
            }
            else
            {
                Debug.LogWarning("No se pudo asignar el animator a esta puerta, asigne uno o desactive el controledByAnimator");
                controledByAnimator = false;
            }
        }
    }

    private void Start()
    {
        _opening = false;
        _currTime = 0;
    }

    private void Update()
    {
        if(_opening && _currTime < time)
        {
            transform.position += Vector3.up * speedShift * Time.deltaTime;
            _currTime += Time.deltaTime;
        }
    }

    public void TriggerOpen()
    {
        if(controledByAnimator)
        {
            anim.SetTrigger("Open");
        }
        else
        {
            _opening = true;
        }
    }

    public void TriggerClose()
    {
        if (controledByAnimator)
        {
            anim.SetTrigger("Close");
        }
        else
        {
            _opening = false;
        }
    }
}
