using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack4State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackIndex = 3;
        duration = 0.5f;

        //lock on, triggers animation, set isAttacking
        playerController.SimpleLockOn();
        playerController.anim.SetTrigger("Attack" + attackIndex);
        playerController.StartCoroutine(playerController.AttackTiming(duration));

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
        if (fixedTime >= duration)
        {

            //atk button pressed
            if (shouldCombo)
            {
                stateMachine.SetNextState(new Attack1State());
            }
            //skill button pressed
            else if (shouldSkill)
            {
                stateMachine.SetNextState(new Skill4State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}
