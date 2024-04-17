using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeBaseState : State
{

    //duration doesnt need to be the same as animation length, make it slightly shorter to transition early to next attack
    protected bool shouldCombo;
    protected bool shouldSkill;
    protected int attackIndex;
    protected int skillIndex;
    protected float duration;

    protected bool shouldDash;
    protected float dashDuration;
    protected float dashCooldown;

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
        Actions.OnSkillButtonPressed += OnSkillButtonPressed;
        Actions.OnDashButtonPressed += SetDashState;
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
        Actions.OnSkillButtonPressed -= OnSkillButtonPressed;
        Actions.OnDashButtonPressed -= SetDashState;
    }

    //linked to attack button action
    public void OnAttackButtonPressed()
    {
        shouldCombo = true;
        shouldSkill = false;
    }
    public void OnSkillButtonPressed()
    {
        shouldSkill = true;
        shouldCombo = false;
    }
    //linked to dash button action
    public void SetDashState()
    {
        shouldDash = true;
    }


}
