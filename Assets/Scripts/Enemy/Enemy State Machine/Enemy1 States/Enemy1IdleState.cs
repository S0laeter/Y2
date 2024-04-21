using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1IdleState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        //move around
        enemyController.anim.SetTrigger("Idle");

        Debug.Log("enemy in idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        enemyController.LookAtPlayer();

        //transition to next state, only based on condition
        enemyStateMachine.SetNextState(new Enemy1StallState());

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition
            

        }
    }

}
