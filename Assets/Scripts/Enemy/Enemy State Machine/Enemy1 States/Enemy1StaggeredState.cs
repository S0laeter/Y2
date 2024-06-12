using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1StaggeredState : Enemy1BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);
        
        stateDuration = 0.75f;

        enemyController.anim.SetTrigger("Staggered");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition


        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            enemyStateMachine.SetNextState(new Enemy1IdleState());

            //transition to next state, based on both stateDuration and condition
            

        }
    }

}
