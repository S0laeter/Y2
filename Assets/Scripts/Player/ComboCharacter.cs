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

    private void OnEnable()
    {
        Actions.OnAttackInput += SetFirstAttackState;
        Actions.OnSkillInput += SetFirstSkillState;
        Actions.OnDashInput += SetDashState;
    }
    private void DisEnable()
    {
        Actions.OnAttackInput -= SetFirstAttackState;
        Actions.OnSkillInput -= SetFirstSkillState;
        Actions.OnDashInput -= SetDashState;
    }

    // Start is called before the first frame update
    void Awake()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        playerController = GetComponent<PlayerController>();

        //set attack power
        attackPower = 1f;
    }





    //animation events on the hitbox
    //pass damage and knockback at the very start of the animation
    public void PassHitboxDamage(float setMultiplier)
    {
        float damage = attackPower * setMultiplier;
        Actions.PassHitboxDamage(damage);
    }
    public void PassHitboxHorizontalKnockback(float setKnockback)
    {
        Actions.PassHitboxKnockback(setKnockback);
    }

    //use this to phase through enemies during certain attacks
    public void EnableIgnoreCollisionWithEnemy()
    {
        Physics.IgnoreLayerCollision(6, 10, true);
    }
    public void DisableIgnoreCollisionWithEnemy()
    {
        Physics.IgnoreLayerCollision(6, 10, false);
    }




    //button actions
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
            meleeStateMachine.SetNextState(new Skill00State());
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
