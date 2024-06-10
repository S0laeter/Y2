using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack4State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackIndex = 4;
        attackDuration = 1.27f;

        //lock on, triggers animation, set isAttacking
        playerController.SimpleLockOn();
        playerController.anim.SetTrigger("Attack" + attackIndex);
        playerController.GainEnergy(10);
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
