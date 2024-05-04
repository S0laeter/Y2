using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        dashDuration = 0.25f;
        dashCooldown = 0.5f;

        //just to make sure dash canceling doesn't break anything
        comboCharacter.DisableIgnoreCollisionWithEnemy();

        playerController.anim.SetTrigger("Dash");
        playerController.StartCoroutine(playerController.DashTiming(dashDuration));
        playerController.StartCoroutine(playerController.DashCooldown(dashCooldown));

        Debug.Log("dash");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

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
