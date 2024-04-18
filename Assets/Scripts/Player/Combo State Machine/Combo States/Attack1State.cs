using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackIndex = 1;
        attackDuration = 0.5f;

        //lock on, triggers animation, set isAttacking
        playerController.SimpleLockOn();
        playerController.anim.SetTrigger("Attack" + attackIndex);
        playerController.StartCoroutine(playerController.AttackTiming(attackDuration));

        Debug.Log("attack " + attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //dash button pressed
        if (shouldDash)
        {
            stateMachine.SetNextState(new DashState());
        }

        //whether or not to continue combo
        if (fixedTime >= attackDuration)
        {

            //atk button pressed
            if (shouldCombo)
            {
                stateMachine.SetNextState(new Attack2State());
            }
            //skill button pressed
            else if (shouldSkill)
            {
                stateMachine.SetNextState(new Skill1State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}
