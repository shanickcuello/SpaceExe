using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHoldGate : MonoBehaviour
{
    public float speedShift;
    public Vector3 finalPosOffset;

    bool _holding;
    float _shiftValue;
    Vector3 _initPos;
    Vector3 _finalPos;


    void Start()
    {
        _initPos = transform.position;
        //_finalPos = _initPos + finalPosOffset;
        _holding = false;
        _shiftValue = 0;
    }

    void Update()
    {
        _finalPos = _initPos + finalPosOffset;

        if (_holding && _shiftValue < 1)
        {
            _shiftValue += speedShift * Time.deltaTime;
        }
        else if (!_holding && _shiftValue > 0)
        {
            _shiftValue -= speedShift * Time.deltaTime;
        }
        Movement();
    }

    void Movement()
    {
        transform.position = Vector3.Lerp(_initPos, _finalPos, _shiftValue);
    }

    public void EnableHoldOpen()
    {
        if (!_holding)
        {
            _holding = true;
        }
    }

    public void DisableHoldOpen()
    {
        if (_holding)
        {
            _holding = false;
        }
    }
}
