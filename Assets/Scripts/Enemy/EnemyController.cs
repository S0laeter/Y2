using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public NavMeshAgent navMeshAgent;
    public Animator anim;
    private Rigidbody rb;

    public GameObject player;
    private float distanceFromPlayer;

    public bool closeToPlayer;
    public bool farFromPlayer;

    public float maxHealth;
    public float currentHealth;

    public float extraTimeOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindWithTag("Player");

        //set the first state, because the stupid state machine just doesnt wanna do it for some reason
        switch (enemyStateMachine.enemyType)
        {
            case "Enemy1":
                enemyStateMachine.SetNextState(new Enemy1IdleState());
                break;
            case "Enemy2":
                enemyStateMachine.SetNextState(new Enemy2IdleState());
                break;
            case "Enemy3":
                //enemyStateMachine.SetNextState(new Enemy3IdleState());
                break;
            case "Boss1":
                //enemyStateMachine.SetNextState(new Boss1IdleState());
                break;
            default:
                break;
        }

        currentHealth = maxHealth;
    }


    void FixedUpdate()
    {

        if (currentHealth <= 0f)
        {
            Die();
        }


        if (player == null)
            return;
        //calculate distance from player
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        //choose actions based on distance
        if (distanceFromPlayer <= navMeshAgent.stoppingDistance)
        {
            closeToPlayer = true;
            farFromPlayer = false;
        }
        else if (distanceFromPlayer > navMeshAgent.stoppingDistance)
        {
            farFromPlayer = true;
            closeToPlayer = false;
        }
        else
        {
            closeToPlayer = false;
            farFromPlayer = false;
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        Debug.Log(this.name + " has taken " + damage + " damage");
    }

    public IEnumerator TakeKnockback(float horizontalKnockback, float verticalKnockback)
    {
        //only low weight mobs will take knockback
        if (rb.mass <= 10f)
        {
            //disable navmesh agent
            navMeshAgent.enabled = false;

            //knockback
            rb.AddForce(transform.forward * -horizontalKnockback);
            rb.AddForce(transform.up * verticalKnockback);

            Debug.Log(this.name + " has taken " + horizontalKnockback + " knockback and " + verticalKnockback + " knockup");

            //wait while knockback
            yield return new WaitForSeconds(0.1f);

            //enable navmesh agent
            navMeshAgent.Warp(this.transform.position);
            navMeshAgent.enabled = true;
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        Actions.OnEnemyKilled(this);

        //destroy this enemy, do this last
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
