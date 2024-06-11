using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2BaseState : EnemyState
{
    protected EnemyController enemyController;

    protected float stateDuration;

    protected bool shouldDie;

    //reminder, random range for int is maxExclusive, so the maximum gotta be one int higher
    protected int randomNextAction;

    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        //getting stuffs
        enemyController = GetComponent<EnemyController>();

        shouldDie = false;

        Actions.OnEnemyKilled += Die;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (shouldDie)
        {
            enemyStateMachine.SetNextState(new Enemy2DeathState());
            return;
        }

        if (!enemyController.isDead)
        {
            //staggered upon being hit if armor is broken
            if (enemyController.shouldGetStaggered)
            {
                enemyController.shouldGetStaggered = false;
                enemyStateMachine.SetNextState(new Enemy2StaggeredState());
            }
            else if (enemyController.shouldGetLaunched)
            {
                enemyController.shouldGetLaunched = false;
                enemyStateMachine.SetNextState(new Enemy2LaunchedState());
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();

        Actions.OnEnemyKilled -= Die;
    }

    private void Die(EnemyController enemyControllerCalledFrom)
    {
        if (enemyControllerCalledFrom == enemyController)
        {
            shouldDie = true;
        }
        
    }


}
