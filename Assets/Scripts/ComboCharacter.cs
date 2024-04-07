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
        Actions.OnDashButtonPressed += SetDashState;
    }
    private void DisEnable()
    {
        Actions.OnAttackButtonPressed -= SetFirstAttackState;
        Actions.OnDashButtonPressed -= SetDashState;
    }

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        playerController = GetComponent<PlayerController>();

        attackPower = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //animation event
    public void SetMultiplier(float setMultiplier)
    {
        damage = attackPower * setMultiplier;
        Actions.HitboxDamage(damage);
    }

    //linked to attack button action
    public void SetFirstAttackState()
    {

        if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new Attack1State());
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
                StartCoroutine(DashTiming());
            }
        }

    }


    private IEnumerator DashTiming()
    {
        playerController.canDash = false;
        playerController.isDashing = true;

        //animation length
        yield return new WaitForSeconds(playerController.dashTime);
        playerController.isDashing = false;

        //cooldown
        yield return new WaitForSeconds(playerController.dashCooldown);
        playerController.canDash = true;

    }

}
