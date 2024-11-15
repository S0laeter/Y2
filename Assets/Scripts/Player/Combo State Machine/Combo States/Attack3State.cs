using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackIndex = 3;
        attackDuration = 1f;

        //lock on, triggers animation, set isAttacking
        playerController.ResetLockOn();
        playerController.LookLockOn();

        playerController.anim.SetTrigger("Attack" + attackIndex);

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

            //atk button pressed
            if (shouldCombo)
            {
                stateMachine.SetNextState(new Attack4State());
            }
            //skill button pressed
            else if (shouldSkill)
            {
                stateMachine.SetNextState(new Skill3State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}
