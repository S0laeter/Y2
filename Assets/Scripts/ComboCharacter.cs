using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;
    private PlayerController playerController;

    public Transform hitbox;
    public float testRange;

    [SerializeField]
    private float attackPower;
    private float damage;

    private void OnEnable()
    {
        Actions.OnAttackButtonPressed += SetFirstAttackState;
    }
    private void DisEnable()
    {
        Actions.OnAttackButtonPressed -= SetFirstAttackState;
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
    public void SetDamage(float setMultiplier)
    {
        damage = attackPower * setMultiplier;
        Actions.HitboxDamage(damage);
    }

    //linked to button action
    public void SetFirstAttackState()
    {

        if (!playerController.isDashing)
        {
            if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
            {
                meleeStateMachine.SetNextState(new Attack1State());
            }
        }

    }

}
