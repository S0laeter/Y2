using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public NavMeshAgent navMeshAgent;
    public Animator anim;
    public Rigidbody rb;

    public GameObject player;
    private float distanceFromPlayer;

    public bool closeToPlayer;
    public bool farFromPlayer;

    private float attackPower;
    public float attackDamage;

    public float maxHealth;
    public float currentHealth;

    public float maxArmor;
    public float currentArmor;
    public bool brokenArmor;

    public bool receivedLightHit;
    public bool receivedHeavyHit;

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
            default:
                break;
        }
        
        attackPower = 1f;
        currentHealth = maxHealth;
        currentArmor = maxArmor;

        brokenArmor = false;
        receivedLightHit = false;
    }


    void Update()
    {
        //regen armor over time
        currentArmor = Mathf.Clamp(currentArmor + 2 * Time.deltaTime, 0f, maxArmor);

        //check health and armor
        if (currentHealth <= 0f)
        {
            Die();
        }
        if (currentArmor <= 10f)
        {
            StartCoroutine(BrokenArmorState());
        }


        if (player == null)
            return;
        //calculate distance from player
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        //choose actions based on distance
        if (distanceFromPlayer > navMeshAgent.stoppingDistance)
        {
            farFromPlayer = true;
            closeToPlayer = false;
        }
        else if (distanceFromPlayer <= navMeshAgent.stoppingDistance)
        {
            closeToPlayer = true;
            farFromPlayer = false;
        }

    }


    //broken armor timer
    public IEnumerator BrokenArmorState()
    {
        brokenArmor = true;
        yield return new WaitForSeconds(4f);
        brokenArmor = false;
    }




    //animation event to update attack damage
    public void PassHitboxDamage(float multiplier)
    {
        attackDamage = attackPower * multiplier;
    }





    public void TakeDamage(float damage, string hitboxType)
    {
        //deduct health
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        //deduct armor, but only when armor is not broken
        if (brokenArmor == false)
        {
            currentArmor = Mathf.Clamp(currentArmor - damage, 0f, maxArmor);
        }

        //which kind of damage received, for the stagger states
        switch (hitboxType)
        {
            case "Hitbox":
                receivedLightHit = true;
                receivedLightHit = false;
                break;
            case "HitboxHeavy":
                receivedHeavyHit = true;
                receivedHeavyHit = false;
                break;
            default:
                break;
        }

        Debug.Log(this.name + " has taken " + damage + " damage");
    }
    public IEnumerator TakeKnockback(float horizontalKnockback)
    {
        //only low weight mobs will take knockback
        if (rb.mass <= 10f)
        {
            //disable navmesh agent
            navMeshAgent.enabled = false;

            //knockback
            rb.AddForce(transform.forward * -horizontalKnockback);

            Debug.Log(this.name + " has taken " + horizontalKnockback + " knockback");

            //wait while knockback
            yield return new WaitForSeconds(0.2f);

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
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);

    }



}
