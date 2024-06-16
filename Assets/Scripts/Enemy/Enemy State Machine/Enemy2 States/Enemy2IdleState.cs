using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2IdleState : Enemy2BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        randomNextAction = Random.Range(0, 2);

        enemyController.anim.SetTrigger("Idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (enemyController.player == null)
        {
            return;
        }

        enemyController.LookAtPlayer();

        //transition to next state, only based on condition
        if (enemyController.closeToPlayer)
        {

            switch (randomNextAction)
            {
                case 0:
                    enemyStateMachine.SetNextState(new Enemy2Attack2State());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new Enemy2StallState());
                    break;
                default:
                    break;
            }

        }
        else if (enemyController.farFromPlayer)
        {

            switch (randomNextAction)
            {
                case 0:
                    enemyStateMachine.SetNextState(new Enemy2StallState());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new Enemy2ApproachState());
                    break;
                default:
                    break;
            }

        }

    }

}
