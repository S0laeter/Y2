using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : EnemyState
{
    protected EnemyController enemyController;

    protected float stateDuration;

    //reminder, random range for int is maxExclusive, so the maximum gotta be one int higher
    protected int randomNextAction;

    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        //getting stuffs
        enemyController = GetComponent<EnemyController>();

        //subscribing to actions
        Actions.OnEnemyKilled += Die;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //when not already dead, and gets hit
        if (enemyStateMachine.currentState.GetType() != typeof(Enemy1DeathState))
        {
            //staggered upon being hit if armor is broken
            if (enemyController.shouldGetStaggered)
            {
                enemyController.shouldGetStaggered = false;
                enemyStateMachine.SetNextState(new Enemy1StaggeredState());
            }
            else if (enemyController.shouldGetLaunched)
            {
                enemyController.shouldGetLaunched = false;
                enemyStateMachine.SetNextState(new Enemy1LaunchedState());
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();

        //unsubscribing to actions
        Actions.OnEnemyKilled -= Die;
    }

    protected void Die(EnemyController enemyController)
    {
        enemyStateMachine.SetNextState(new Enemy1DeathState());
    }


}
