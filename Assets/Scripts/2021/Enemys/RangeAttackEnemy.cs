using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackEnemy : AV, IGridEntity
{
    Device deviceTarget, newTarget;
    [SerializeField] Device startDevice, endDevice;
    float currentCounter;
    [SerializeField] float counterToAttack;
    bool followPlayer;

    [SerializeField] float timeToSearchPlayer;
    SquareQuery squareQuery;

    Device nextDeviceToGo;

    public event Action<IGridEntity> OnMove;

    public Vector3 Position
    {
        get => transform.position;
        private set => transform.position = value;
    }

    protected override void Awake()
    {
        base.Awake();
        squareQuery = GetComponent<SquareQuery>();
    }

    protected override void Update()
    {
        if ((transform.position == _targetPosition) && !_arrived)
            ArrivedMethod();

        if (!_arrived)
            Movement();

        if (_arrived)
        {
            _currTimeInDevice -= Time.deltaTime;

            if (_currTimeInDevice <= (timeInDevice / 2) && !_line.enabled)
            {
                int _tempDestIndex = destinyIndex + 1;
                if (_tempDestIndex >= devices.Count)
                {
                    _tempDestIndex = 0;
                }

                _line.enabled = true;
                _line.SetPosition(0, transform.position);
                _line.SetPosition(1, devices[_tempDestIndex].transform.position);
            }

            if (_currTimeInDevice <= 0)
            {
                if (SearchPlayer())
                {
                    SettingNewDestiny(playerSCR.device);
                }
                else
                {
                    SettingNewDestiny();
                }
                _line.enabled = false;
                _currDevice.OnEntityExit();
                destinyIndex++;
                if (destinyIndex >= devices.Count)
                {
                    destinyIndex = 0;
                }
            }
        }
        Attack();
    }



    bool SearchPlayer() //IA2-P2 este codigo se ejecuta solo cuando este enemigo esta por saltar a un nuevo destino. No se ejecuta en update.
    {
        foreach (var item in squareQuery.Query()) 
        {
            if (item.Equals(playerSCR))
            {
                Debug.Log("Encontre al player");
                return true;
            }
        }
        return false;
    }

}
