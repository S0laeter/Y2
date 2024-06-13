using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackDuration = 3f;

        playerController.anim.SetTrigger("Ult");
        playerController.GainEnergy(-100);

        playerController.StartCoroutine(playerController.AttackTiming(attackDuration));
        playerController.StartCoroutine(playerController.DashTiming(attackDuration));

        Debug.Log("NEJNENDBWASIDCSHDIFHEUDOWDJAWODCASNDSANDKJWD");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //whether or not to continue combo
        if (fixedTime >= attackDuration)
        {

            stateMachine.SetNextStateToMain();

        }
    }
}
