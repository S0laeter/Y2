using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallState : EnemyBaseState
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
            if (enemyController.shouldAttack)
            {

                //choose a random attack
                switch (randomNextAction)
                {
                    case 0:
                        enemyStateMachine.SetNextState(new EnemyAttack1State());
                        break;
                    case 1:
                        enemyStateMachine.SetNextState(new EnemyAttack2State());
                        break;
                    default:
                        break;
                }

            }
            else if (enemyController.shouldApproach)
            {
                enemyStateMachine.SetNextState(new ApproachState());
            }

        }
    }

}
