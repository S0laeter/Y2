using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;
    private PlayerController playerController;

    public GameObject swordwavePrefab;
    public Transform instantiatePoint;

    [SerializeField]
    private float attackPower;

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





    //animation events on the hitbox
    //pass damage and knockback at the very start of the animation
    public void PassHitboxDamage(float setMultiplier)
    {
        float damage = attackPower * setMultiplier;
        Actions.PassHitboxDamage(damage);
    }
    public void PassHitboxHorizontalKnockback(float setKnockback)
    {
        Actions.PassHitboxHorizontalKnockback(setKnockback);
    }
    public void PassHitboxVerticalKnockback(float setKnockback)
    {
        Actions.PassHitboxVerticalKnockback(setKnockback);
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

    //instantiate swordwave
    public void InstantiateSwordwave()
    {
        //instantiate, remember to rotate the point during animation to tilt the swordwave
        GameObject swordwave = Instantiate(swordwavePrefab, instantiatePoint.position, instantiatePoint.rotation);

        //swordwave speed
        Rigidbody swordwaveRb = swordwave.GetComponent<Rigidbody>();
        swordwaveRb.velocity = transform.TransformDirection(Vector3.forward * 20f);

        //pass the damage and knockback values as well
        float damage = attackPower * 20f;
        Actions.PassProjectileDamage(damage);
        Actions.PassProjectileHorizontalKnockback(10f);
        Actions.PassProjectileVerticalKnockback(0f);
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
