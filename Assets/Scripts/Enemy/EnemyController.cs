using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class EnemyController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;
    public NavMeshAgent navMeshAgent;
    public Animator anim;
    public Rigidbody rb;

    public GameObject player;
    private float distanceFromPlayer;
    
    public GameObject bulletPrefab;
    public Transform instantiatePoint;

    public bool closeToPlayer;
    public bool farFromPlayer;

    private float attackPower;
    public float attackDamage;

    public float maxHealth;
    public float currentHealth;

    public float maxArmor;
    public float currentArmor;
    public bool brokenArmor;
    private bool canBrokenArmor;
    private float brokenArmorDuration;

    public bool shouldGetStaggered;
    public bool shouldGetLaunched;

    public bool isDead;

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
                enemyStateMachine.SetNextState(new Enemy3IdleState());
                break;
            default:
                break;
        }
        
        attackPower = 1f;

        currentHealth = maxHealth;
        currentArmor = maxArmor;

        brokenArmor = false;
        canBrokenArmor = true;
        brokenArmorDuration = 5f;

        shouldGetStaggered = false;
        shouldGetLaunched = false;

        isDead = false;
    }


    void FixedUpdate()
    {

        //check armor
        //if not broken armor, regen it
        if (!brokenArmor)
        {
            currentArmor = Mathf.Clamp(currentArmor + 1f * Time.deltaTime, 0f, maxArmor);
        }
        //if broken armor, set the timer
        if (Mathf.Floor(currentArmor) <= 0f && canBrokenArmor == true)
        {
            StartCoroutine(BrokenArmorState());
        }



        //enemy ai stuffs
        if (player == null || isDead)
            return;
        //calculate distance from player
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        //choose actions based on distance
        //navmesh's stopping distance is buggy as fuck ngl..
        if (distanceFromPlayer > navMeshAgent.stoppingDistance + 0.1f)
        {
            farFromPlayer = true;
            closeToPlayer = false;
        }
        else if (distanceFromPlayer <= navMeshAgent.stoppingDistance + 0.1f)
        {
            closeToPlayer = true;
            farFromPlayer = false;
        }

    }


    //broken armor timer
    private IEnumerator BrokenArmorState()
    {
        canBrokenArmor = false;
        brokenArmor = true;

        yield return new WaitForSeconds(brokenArmorDuration);

        currentArmor = maxArmor;
        brokenArmor = false;
        canBrokenArmor = true;
    }




    //animation event to update attack damage
    public void PassHitboxDamage(float multiplier)
    {
        attackDamage = attackPower * multiplier;
    }






    public void TakeDamage(float playerDamage, string playerHitboxType)
    {
        //deduct health
        currentHealth = Mathf.Clamp(currentHealth - playerDamage, 0f, maxHealth);
        Actions.OnEnemyDamaged(playerDamage);
        //if 0 health left and is not already dead, go die
        if (currentHealth <= 0f && isDead == false)
        {
            Die();
            return;
        }

        //if armor not broken, reduce armor
        if (!brokenArmor)
        {
            currentArmor = Mathf.Clamp(currentArmor - playerDamage, 0f, maxArmor);
        }
        //if armor already broken, get staggered
        else if (brokenArmor)
        {
            //which kind of damage received, for the stagger states
            switch (playerHitboxType)
            {
                case "Light":
                    shouldGetStaggered = true;
                    break;
                case "Heavy":
                    shouldGetLaunched = true;
                    break;
                default:
                    break;
            }
        }

        Debug.Log(this.name + " has taken " + playerDamage + " damage");
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

            //wait while knockback
            yield return new WaitForEndOfFrame();

            //enable navmesh agent
            navMeshAgent.enabled = true;
        }
    }





    public void Die()
    {
        isDead = true;
        navMeshAgent.enabled = false;

        Actions.OnEnemyKilled(this);

        //destroy this enemy, do this last
        Destroy(this.gameObject, 1.5f);
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


    //instantiate bullet
    public void InstantiateBullet()
    {
        //instantiate, remember to rotate the point during animation to tilt the swordwave
        Instantiate(bulletPrefab, instantiatePoint.position, instantiatePoint.rotation, this.transform);

        //pass the damage
        attackDamage = attackPower * 10f;
    }


}
