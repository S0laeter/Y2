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

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //staggered upon being hit if armor is broken
        if (enemyController.brokenArmor == true)
        {
            if (enemyController.receivedLightHit)
            {
                enemyStateMachine.SetNextState(new Enemy1StaggeredState());
            }
            else if (enemyController.receivedHeavyHit)
            {
                enemyStateMachine.SetNextState(new Enemy1LaunchedState());
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        //unsubscribing to actions

    }



}
