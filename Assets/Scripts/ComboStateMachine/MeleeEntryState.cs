using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : State
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        State nextState = (State)new Attack1State();
        stateMachine.SetNextState(nextState);
    }
}


//this is just in case we have multiple movesets