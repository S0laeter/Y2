using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2StallState : Enemy2BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);
        
        stateDuration = Random.Range(2f, 5f);
        randomNextAction = Random.Range(0, 2);

        enemyController.anim.SetTrigger("Stall");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (enemyController.player == null)
        {
            enemyStateMachine.SetNextEnemyStateToMain();
        }

        enemyController.LookAtPlayer();

        //if navmesh is on, back away
        if (enemyController.navMeshAgent.enabled == true)
        {
            enemyController.transform.position -= enemyController.transform.TransformDirection(Vector3.forward) * Time.deltaTime;
            enemyController.navMeshAgent.Warp(enemyController.transform.position);
        }

        //transition to next state, only based on condition


        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition


            //transition to next state, based on both stateDuration and condition
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
                        enemyStateMachine.SetNextState(new Enemy2Attack1State());
                        break;
                    case 1:
                        enemyStateMachine.SetNextState(new Enemy2ApproachState());
                        break;
                    default:
                        break;
                }

            }

            //if nothing happens, go back to idle
            else
            {
                enemyStateMachine.SetNextEnemyStateToMain();
            }

        }
    }

}
