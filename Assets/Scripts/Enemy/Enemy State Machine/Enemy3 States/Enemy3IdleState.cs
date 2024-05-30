using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3IdleState : Enemy3BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);
        
        enemyController.LookAtPlayer();
        enemyController.anim.SetTrigger("Idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (enemyController.player == null)
        {
            return;
        }

        //transition to next state, only based on condition
        enemyStateMachine.SetNextState(new Enemy3ApproachState());

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition
            

        }
    }

}
