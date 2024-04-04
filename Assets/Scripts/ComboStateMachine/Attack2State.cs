using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //individual atk damage
        //damage = 15;

        attackIndex = 2;
        duration = 0.5f;
        anim.SetTrigger("Attack" + attackIndex);
        Debug.Log("atk " + attackIndex + " executed");
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
