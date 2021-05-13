using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeInFadeOut(bool b)
    {
        if (b)
        {
            _animator.SetTrigger("TurnOn");
        }
        else
        {
            _animator.SetTrigger("TurnOff");
        }
    }
}
