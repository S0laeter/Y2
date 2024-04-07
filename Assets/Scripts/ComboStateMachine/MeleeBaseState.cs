using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeBaseState : State
{

    public float duration;
    protected bool shouldCombo;
    protected int attackIndex;

    protected ComboCharacter comboCharacter;
    protected PlayerController playerController;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        comboCharacter = GetComponent<ComboCharacter>();
        playerController = GetComponent<PlayerController>();

        //subscribing to actions
        Actions.OnAttackButtonPressed += OnAttackButtonPressed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
    }

    public override void OnExit()
    {
        base.OnExit();

        //unsubscribing to actions
        Actions.OnAttackButtonPressed -= OnAttackButtonPressed;
    }

    //linked to button action
    public void OnAttackButtonPressed()
    {
        shouldCombo = true;
    }



}
