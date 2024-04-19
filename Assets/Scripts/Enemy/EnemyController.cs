using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public NavMeshAgent navMeshAgent;
    public Animator anim;

    public GameObject player;
    private float distanceFromPlayer;

    public bool shouldApproach;
    public bool shouldAttack;

    public float maxHealth;
    public float currentHealth;

    public float extraTimeOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0f)
        {
            Die();
        }

        if (player == null)
            return;
        
        

        //calculate distance from player
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        //if too far, run to player
        if (distanceFromPlayer > navMeshAgent.stoppingDistance)
        {
            shouldApproach = true;
            shouldAttack = false;

            if (enemyStateMachine.currentState.GetType() == typeof(IdleEnemyState))
            {
                enemyStateMachine.SetNextState(new ApproachState());
            }
        }
        else
        {
            shouldAttack = true;
            shouldApproach = false;
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        Debug.Log(this.name + " has taken " + damage + " damage");
    }

    public void Die()
    {
        Actions.OnEnemyKilled(this);
        Destroy(this.gameObject);
    }




    public void LookAtPlayer()
    {
        //get direction of player
        Vector3 relativePosition = player.transform.position - this.transform.position;
        //this is so the character doesnt look up or down, only straight forward
        relativePosition.y = 0f;
        //rotate to player, smoothly
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        this.transform.rotation = Quaternion.Slerp(this. transform.rotation, rotation, 0.2f);

    }

}
