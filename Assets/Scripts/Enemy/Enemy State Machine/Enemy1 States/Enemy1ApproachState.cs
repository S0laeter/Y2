using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1ApproachState : Enemy1BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        randomNextAction = Random.Range(0, 4);

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

    }

}
