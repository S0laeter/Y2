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
        duration = 0.5f;
        playerController.anim.SetTrigger("Attack" + attackIndex);
        Debug.Log("atk " + attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //whether or not to continue combo
        if (fixedTime >= duration)
        {

            //atk button pressed
            if (shouldCombo)
            {
                stateMachine.SetNextState(new Attack2State());
            }
            //nothing pressed
            else
            {
                stateMachine.SetNextStateToMain();
            }

        }
    }
}
