using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;
    private PlayerController playerController;

    [SerializeField]
    private float attackPower;
    private float damage;

    private void OnEnable()
    {
        Actions.OnAttackButtonPressed += SetFirstAttackState;
        Actions.OnSkillButtonPressed += SetFirstSkillState;
        Actions.OnDashButtonPressed += SetDashState;
    }
    private void DisEnable()
    {
        Actions.OnAttackButtonPressed -= SetFirstAttackState;
        Actions.OnSkillButtonPressed -= SetFirstSkillState;
        Actions.OnDashButtonPressed -= SetDashState;
    }

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        playerController = GetComponent<PlayerController>();

        //set attack power
        attackPower = 1f;
    }



    //animation event on the hitbox
    public void PassHitboxDamage(float setMultiplier)
    {
        damage = attackPower * setMultiplier;
        Actions.PassHitboxDamage(damage);
    }



    //linked to attack button action
    public void SetFirstAttackState()
    {
        if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new Attack1State());
        }
    }

    //linked to skill button action
    public void SetFirstSkillState()
    {
        if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new Skill1State());
        }
    }

    //linked to dash button action
    public void SetDashState()
    {
        if (playerController.canDash)
        {
            if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
            {
                meleeStateMachine.SetNextState(new DashState());
            }
        }
    }

}
