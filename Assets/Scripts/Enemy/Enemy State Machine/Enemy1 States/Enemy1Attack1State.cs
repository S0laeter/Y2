using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Attack1State : Enemy1BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 2.25f;

        enemyController.navMeshAgent.Warp(enemyController.transform.position);
        enemyController.LookAtPlayer();

        enemyController.anim.SetTrigger("Attack1");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition
        

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            enemyStateMachine.SetNextState(new Enemy1StallState());

            //transition to next state, based on both stateDuration and condition


        }
    }

}
