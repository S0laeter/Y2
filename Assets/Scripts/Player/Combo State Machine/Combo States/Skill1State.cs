using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        skillIndex = 1;
        attackDuration = 0.5f;

        //lock on, triggers animation, set isAttacking
        playerController.SimpleLockOn();
        playerController.anim.SetTrigger("Skill" + skillIndex);
        playerController.StartCoroutine(playerController.AttackTiming(attackDuration));

        Debug.Log("skill " + skillIndex);
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
                stateMachine.SetNextState(new Attack1State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}