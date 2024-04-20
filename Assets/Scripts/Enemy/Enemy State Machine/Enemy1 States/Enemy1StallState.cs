using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1StallState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);
        
        stateDuration = Random.Range(2f, 5f);
        randomNextAction = Random.Range(0, 2);

        //move around
        enemyController.anim.SetTrigger("Stall");

        Debug.Log("enemy backing away for " + stateDuration + " seconds");
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
            if (enemyController.closeToPlayer)
            {

                //choose a random attack
                switch (randomNextAction)
                {
                    case 0:
                        enemyStateMachine.SetNextState(new Enemy1Attack1State());
                        break;
                    case 1:
                        enemyStateMachine.SetNextState(new Enemy1Attack2State());
                        break;
                    default:
                        break;
                }

            }
            else if (enemyController.farFromPlayer)
            {
                enemyStateMachine.SetNextState(new Enemy1ApproachState());
            }

            //if nothing happens, go back to idle
            else
            {
                enemyStateMachine.SetNextEnemyStateToMain();
            }

        }
    }

}
