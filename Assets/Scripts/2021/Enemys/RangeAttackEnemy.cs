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
        base.Update();
        SearchPlayer();
    }

    void SearchPlayer()
    {
        foreach (var item in squareQuery.Query())
        {
            if (item.Equals(playerSCR))
            {
                Debug.LogError("Encontre al player");
            }
        }
    }

}
