using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2DeathState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        enemyController.anim.SetTrigger("Death");

        Debug.Log("enemy died");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition


        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition


        }
    }

}
