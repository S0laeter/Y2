using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack1State : Enemy2BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 2f;

        enemyController.navMeshAgent.Warp(enemyController.transform.position);

        enemyController.anim.SetTrigger("Attack1");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        enemyController.LookAtPlayer();

        //transition to next state, only based on condition
        

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            

            //transition to next state, based on both stateDuration and condition
            if (enemyController.farFromPlayer)
            {
                enemyStateMachine.SetNextState(new Enemy2ApproachState());
            }
            else if (enemyController.closeToPlayer)
            {
                enemyStateMachine.SetNextState(new Enemy2StallState());
            }

        }
    }

}
