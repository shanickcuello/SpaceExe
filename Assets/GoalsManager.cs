using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsManager : MonoBehaviour
{
    public static GoalsManager instance;

    public delegate void goalCondition();
    public List<Goal> goals = new List<Goal>();

    public int playerAmountOfJumps, playerAmountOfJumpsToRecord;
    public bool playerLooseLife;
    public bool playerGetBullet;
    public bool timeRunning;
    public bool playerUseCheckPoint;
    public float timeToWinRecord, currentTime;

    Goal neverLooseLife;
    Goal amountOfJumps;
    Goal superSpeed;
    Goal neverUseCheckPoint;
    Goal neverGetAShoot;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        CreateGoals();
        timeRunning = true;
    }

    private void Update()
    {
        CheckTimeToWinRecord();
    }

    private void CreateGoals()
    {
        neverLooseLife = new Goal(false, "¡Nunca te quitaron vida!", CheckIfPlayerLooseOneTimeLife);
        amountOfJumps = new Goal(false, "¡Realizaste menos de 100 saltos para ganar!", CheckAmountOfJumps);
        superSpeed = new Goal(false, "Ganaste en menos de: " + timeToWinRecord, CheckTimeToWinRecord);
        neverUseCheckPoint = new Goal(false, "¡Nunca utilizaste un checkpoint!", CheckUserUseCheckPoint);
        neverGetAShoot = new Goal(false, "¡Matrix! Nunca te pegó una bala", CheckPlayergetBullet);

        goals.Add(neverLooseLife);
        goals.Add(amountOfJumps);
        goals.Add(superSpeed);
        goals.Add(neverUseCheckPoint);
        goals.Add(neverGetAShoot);
    }

    private void CheckPlayergetBullet()
    {
        if (!playerGetBullet)
        {
            neverGetAShoot.completed = true;
        }
    }

    private void CheckUserUseCheckPoint()
    {
        if (!playerUseCheckPoint)
        {
            neverUseCheckPoint.completed = true;
        }
    }

    private void CheckTimeToWinRecord()
    {
        if (timeRunning)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (currentTime < timeToWinRecord)
            {
                superSpeed.completed = true;
            }
        }
    }

    void CheckIfPlayerLooseOneTimeLife()
    {
        if (!playerLooseLife)
        {
            neverLooseLife.completed = true;
        }
    }

    void CheckAmountOfJumps()
    {
        if (playerAmountOfJumps < playerAmountOfJumpsToRecord)
        {
            amountOfJumps.completed = true;
        }
    }

    public List<string> GetWinGoals()
    {
        List<string> descriptionGoals = new List<string>();
        foreach (var item in goals)
        {
            item.condition.Invoke();
            if (item.completed == true)
                descriptionGoals.Add(item.description);
        }
        return descriptionGoals;
    }
}
