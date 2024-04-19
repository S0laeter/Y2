using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : EnemyBaseState
{
    public override void OnEnter(EnemyStateMachine _enemyStateMachine)
    {
        base.OnEnter(_enemyStateMachine);

        stateDuration = 1f;
        randomNextAction = Random.Range(0, 3);

        enemyController.navMeshAgent.SetDestination(enemyController.player.transform.position);
        enemyController.anim.SetTrigger("Approach");

        Debug.Log("enemy approaching");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //transition to next state, only based on condition
        if (enemyController.shouldAttack)
        {

            //choose a random attack, or just wait and do nothing lmao
            switch (randomNextAction) {
                case 0:
                    enemyStateMachine.SetNextState(new EnemyAttack1State());
                    break;
                case 1:
                    enemyStateMachine.SetNextState(new EnemyAttack2State());
                    break;
                case 2:
                    enemyStateMachine.SetNextState(new StallState());
                    break;
                default:
                    break;
            }
            
        }

        if (fixedTime >= stateDuration)
        {
            //transition to next state, after stateDuration with no condition
            if (enemyController.shouldApproach)
            {
                enemyStateMachine.SetNextState(new ApproachState());
            }

            //transition to next state, based on both stateDuration and condition


        }
    }

}
