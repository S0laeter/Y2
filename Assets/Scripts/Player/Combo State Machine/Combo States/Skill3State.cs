using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        skillIndex = 3;
        attackDuration = 2.2f;

        //lock on, triggers animation, set isAttacking
        playerController.ResetLockOn();
        playerController.LookLockOn();

        playerController.anim.SetTrigger("Skill" + skillIndex);

        playerController.GainEnergy(20);
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
            
            stateMachine.SetNextStateToMain();

        }
    }
}
