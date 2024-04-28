using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ApproachState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 1f;
        randomNextAction = Random.Range(0, 3);
        
        enemyController.anim.SetTrigger("Approach");
        
        Debug.Log("enemy approaching");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if navmesh is on, chase player. if not, return
        if (enemyController.navMeshAgent.enabled == true)
        {
            enemyController.navMeshAgent.SetDestination(enemyController.player.transform.position);
        }
        else
            return;

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
                    enemyStateMachine.SetNextState(new Enemy1StallState());
                    break;
                default:
                    break;
            }
            
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            if (enemyController.farFromPlayer)
            {
                enemyStateMachine.SetNextState(new Enemy1ApproachState());
            }

            //transition to next state, based on both stateDuration and condition


            //if nothing happens, go back to idle
            else
            {
                enemyStateMachine.SetNextEnemyStateToMain();
            }

        }
    }

}
