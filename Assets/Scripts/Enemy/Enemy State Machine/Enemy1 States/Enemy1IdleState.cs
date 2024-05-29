using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1IdleState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        randomNextAction = Random.Range(0, 4);

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
                    enemyStateMachine.SetNextState(new Enemy1Attack1State());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new Enemy1Attack2State());
                    break;
                case 2:
                    enemyStateMachine.SetNextState(new Enemy1Attack3State());
                    break;
                case 3:
                    enemyStateMachine.SetNextState(new Enemy1StallState());
                    break;
                default:
                    break;
            }

        }
        else if (enemyController.farFromPlayer)
        {
            enemyStateMachine.SetNextState(new Enemy1ApproachState());
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition
            

        }
    }

}
