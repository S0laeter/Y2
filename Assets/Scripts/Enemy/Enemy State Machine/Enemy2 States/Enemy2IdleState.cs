using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2IdleState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);
        
        randomNextAction = Random.Range(0, 2);

        //move around
        enemyController.anim.SetTrigger("Idle");

        Debug.Log("enemy in idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        enemyController.LookAtPlayer();

        //transition to next state, only based on condition
        if (enemyController.closeToPlayer)
        {

            //choose a random attack
            switch (randomNextAction)
            {
                case 0:
                    enemyStateMachine.SetNextState(new Enemy2Attack1State());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new Enemy2Attack2State());
                    break;
                default:
                    break;
            }

        }
        else if (enemyController.farFromPlayer)
        {
            enemyStateMachine.SetNextState(new Enemy2ApproachState());
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition
            

        }
    }

}
