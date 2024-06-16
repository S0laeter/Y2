using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2DeathState : Enemy2BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 1.5f;

        enemyController.anim.SetTrigger("Death");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition


    }

}
