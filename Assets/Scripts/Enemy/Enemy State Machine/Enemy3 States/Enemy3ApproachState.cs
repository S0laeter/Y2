using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3ApproachState : Enemy3BaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

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
            enemyStateMachine.SetNextState(new Enemy3Attack1State());
        }

    }

}
