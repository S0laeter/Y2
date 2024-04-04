using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeBaseState : State
{

    public Animator anim;

    //public float damage;

    public float duration;
    protected bool shouldCombo;
    protected int attackIndex;

    protected Transform hitboxPoint;

    private ComboCharacter combo;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        anim = GetComponent<Animator>();
        combo = GetComponent<ComboCharacter>();
        hitboxPoint = combo.hitbox;

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
