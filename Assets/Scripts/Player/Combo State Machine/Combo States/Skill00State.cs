using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill00State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        skillIndex = 0;
        attackDuration = 0.76f;

        //lock on, triggers animation, set isAttacking
        playerController.ResetLockOn();
        playerController.LookLockOn();

        playerController.anim.SetTrigger("Skill" + skillIndex + "-0");

        playerController.GainEnergy(5);
        playerController.StartCoroutine(playerController.AttackTiming(attackDuration));
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

            if (shouldSkill)
            {
                stateMachine.SetNextState(new Skill01State());
            }
            else if (shouldCombo)
            {
                stateMachine.SetNextState(new Attack3State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}
