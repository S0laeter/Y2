using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2ApproachState : Enemy2BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 3f;
        randomNextAction = Random.Range(0, 3);

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
        if (enemyController.farFromPlayer)
        {
            if (enemyController.navMeshAgent.enabled == false)
                return;
            enemyController.navMeshAgent.SetDestination(enemyController.player.transform.position);
        }
        else
        {

            switch (randomNextAction) {
                case 0:
                    enemyStateMachine.SetNextState(new Enemy2Attack1State());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new Enemy2Attack2State());
                    break;
                case 2:
                    enemyStateMachine.SetNextState(new Enemy2StallState());
                    break;
                default:
                    break;
            }
            
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            

            //transition to next state, based on both stateDuration and condition
            if (enemyController.farFromPlayer)
            {
                enemyStateMachine.SetNextState(new Enemy2Attack1State());
            }

        }
    }

}
