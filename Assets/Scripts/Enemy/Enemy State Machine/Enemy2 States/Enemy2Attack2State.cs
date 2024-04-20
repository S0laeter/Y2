using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack2State : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 2f;

        enemyController.LookAtPlayer();
        enemyController.anim.SetTrigger("Attack2");

        Debug.Log("enemy doing attack 2");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition
        

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            enemyStateMachine.SetNextState(new Enemy2StallState());

            //transition to next state, based on both stateDuration and condition


        }
    }

}