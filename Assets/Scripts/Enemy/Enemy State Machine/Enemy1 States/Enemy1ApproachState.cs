using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ApproachState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        randomNextAction = Random.Range(0, 5);

        enemyController.anim.SetTrigger("Approach");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (enemyController.player == null)
        {
            enemyStateMachine.SetNextEnemyStateToMain();
        }

        //if navmesh is on, chase player
        if (enemyController.navMeshAgent.enabled == true)
        {
            enemyController.navMeshAgent.SetDestination(enemyController.player.transform.position);
        }
        else
        {
            enemyStateMachine.SetNextState(new Enemy1ApproachState());
        }

        //transition to next state, only based on condition
        if (enemyController.closeToPlayer)
        {
            //choose a random attack, or just wait and do nothing lmao
            switch (randomNextAction) {
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
                case 4:
                    enemyStateMachine.SetNextEnemyStateToMain();
                    break;
                default:
                    break;
            }
            
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            

            //transition to next state, based on both stateDuration and condition


        }
    }

}
