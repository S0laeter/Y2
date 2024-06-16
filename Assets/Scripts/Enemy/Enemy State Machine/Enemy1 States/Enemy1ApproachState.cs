using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ApproachState : Enemy1BaseState
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

        //transition to next state, only based on condition
        if (enemyController.closeToPlayer)
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
                case 4:
                    enemyStateMachine.SetNextEnemyStateToMain();
                    break;
                default:
                    break;
            }
            
        }

    }

}
