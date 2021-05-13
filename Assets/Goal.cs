using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public bool completed;
    public string description;
    public GoalsManager.goalCondition condition;

    public Goal(bool completed, string description, GoalsManager.goalCondition condition)
    {
        this.completed = completed;
        this.description = description;
        this.condition = condition;
    }

}
