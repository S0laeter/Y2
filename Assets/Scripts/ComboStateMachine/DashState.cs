using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        duration = playerController.dashTime;
        playerController.anim.SetTrigger("Dash");
        Debug.Log("dash");
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
