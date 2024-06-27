using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rb;

    public Transform cam;

    public PlayerInput playerInput;
    public FixedJoystick joystick;
    private Vector2 joystickDirection;
    
    private float moveSpeed;
    float turnSmoothVelocity;
    public bool isRunning;

    public bool isAttacking;

    public bool canDash = true;
    public bool isDashing;

    public float maxHealth = 100f;
    public float currentHealth;
    private bool isDead;

    public float maxEnergy = 100f;
    public float currentEnergy;

    private bool skillHolding;
    private float skillHeldTime;
    private float skillHoldTimeTarget = 0.4f;

    public GameObject coneLockOnBase;
    private Collider lockedOnEnemy;

    private void OnEnable()
    {
        Actions.OnTimeOut += Die;

        Actions.OnEnemyDamaged += LifeSteal;
    }
    private void DisEnable()
    {
        Actions.OnTimeOut -= Die;

        Actions.OnEnemyDamaged -= LifeSteal;

        //input events
        playerInput.actions["Dash"].performed -= DashInputPerformed;
        playerInput.actions["Attack"].performed -= AttackInputPerformed;
        playerInput.actions["Skill"].started -= SkillInputStarted;
        playerInput.actions["Skill"].canceled -= SkillInputCanceled;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        cam = GameObject.FindWithTag("MainCamera").transform;

        //input events
        playerInput.actions["Dash"].performed += DashInputPerformed;
        playerInput.actions["Attack"].performed += AttackInputPerformed;
        playerInput.actions["Skill"].started += SkillInputStarted;
        playerInput.actions["Skill"].canceled += SkillInputCanceled;

        moveSpeed = 200f;

        currentHealth = maxHealth;
        isDead = false;
        currentEnergy = 0f;
        Actions.UpdatePlayerHealthBar(this);
        Actions.UpdatePlayerEnergyBar(this);

        skillHeldTime = 0f;
        skillHolding = false;
    }

    //input stuffs
    private void DashInputPerformed(InputAction.CallbackContext context)
    {
        Actions.OnDashInput();
    }
    private void AttackInputPerformed(InputAction.CallbackContext context)
    {
        Actions.OnAttackInput();
    }
    private void SkillInputStarted(InputAction.CallbackContext context)
    {
        skillHolding = true;
    }
    private void SkillInputCanceled(InputAction.CallbackContext context)
    {
        if (skillHeldTime < skillHoldTimeTarget)
        {
            Actions.OnSkillInput();
        }

        skillHolding = false;
    }
    

    // Update is called once per frame
    void Update()
    {

        //while holding skill
        if (skillHolding)
        {
            skillHeldTime += Time.deltaTime;

            if (skillHeldTime >= skillHoldTimeTarget)
            {
                Actions.OnUltInput();
                skillHolding = false;
            }
        }
        else
        {
            skillHeldTime = 0f;
        }

        //check if dead
        if (currentHealth <= 0f && !isDead)
        {
            isDead = true;
            currentHealth = 0;

            Die();
        }

    }

    void FixedUpdate()
    {

        //cant move while dashing or attacking
        if (isDashing || isAttacking)
        {
            return;
        }

        PlayerMovement();

    }

    private void PlayerMovement()
    {

        //get the joystick rotation, normalized so it stays at the same speed
        joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;

        //just get the direction first, then do whatever later
        Vector3 direction = new Vector3(joystickDirection.x, 0f, joystickDirection.y).normalized;

        //when moving or not
        if (direction.magnitude > 0)
        {

            //run animation
            anim.SetBool("Running", true);

            //look rotation
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //smooth out the angle (only for smooth turning with keyboard stuffs but imma just include it anyway)
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, 0.1f);
            //then rotate to look
            this.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //now move
            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.velocity = moveDirection.normalized * moveSpeed * Time.deltaTime;
            
            isRunning = true;

        }
        else
        {
            //idle animation
            anim.SetBool("Running", false);
            anim.SetTrigger("Idle");

            isRunning = false;
        }

    }

    //life steal, btw dont make this too op lmao
    public void LifeSteal(float healthToAdd)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthToAdd/2, 0f, maxHealth);

        Actions.UpdatePlayerHealthBar(this);
    }
    //take damage, but invincible while dashing
    public void TakeDamage(float damage)
    {
        if (!isDashing)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

            Actions.UpdatePlayerHealthBar(this);
        }
    }
    //get knocked back from heavy hit
    public void TakeHeavyHit()
    {
        if (!isDashing)
        {
            //GOTTA ADD STAGGER ANIMATION LATER
        }
        
    }

    //add energy
    public void GainEnergy(float energyToAdd)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + energyToAdd, 0f, maxEnergy);

        Actions.UpdatePlayerEnergyBar(this);
    }

    //die
    public void Die()
    {
        anim.SetTrigger("Death");

        Actions.Lose();
    }





    //attacking check, called from MeleeBaseState
    public IEnumerator AttackTiming(float attackTime)
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    //dashing check, called from DashState
    public IEnumerator DashTiming(float dashDuration)
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
    //dash cooldown, called from DashState
    public IEnumerator DashCooldown(float dashCooldown)
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }




    //if they actively want to target something, take enemy in that direction
    //if they dont target anything, just take the closest enemy
    public void ResetLockOn()
    {
        
        //array of colliders in a sphere, in layer Enemy
        Collider[] enemiesInSphere = new Collider[100];
        //get the enemies in sphere and sort by distance
        enemiesInSphere = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Enemy"));
        //if nothing in range, nvm fuck it
        if (enemiesInSphere.Length == 0)
            return;
        enemiesInSphere = enemiesInSphere.OrderBy((enemy) => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();

        //array of colliders in a rectangle in front, in layer Enemy
        Collider[] enemiesInBox = new Collider[100];
        //if player is trying to change direction
        joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        Vector3 direction = new Vector3(joystickDirection.x, 0f, joystickDirection.y).normalized;
        if (direction.magnitude > 0)
        {
            //rotate base
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            coneLockOnBase.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //get the enemies in box and sort by distance
            enemiesInBox = Physics.OverlapBox(coneLockOnBase.transform.position, coneLockOnBase.transform.localScale / 2, coneLockOnBase.transform.rotation, LayerMask.GetMask("Enemy"));
            enemiesInBox = enemiesInBox.OrderBy((enemy) => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();

            //assign lock on enemy
            lockedOnEnemy = enemiesInBox[0];
        }
        else
        {
            //assign lock on enemy
            lockedOnEnemy = enemiesInSphere[0];
        }

        //clear arrays for next lock on reset
        System.Array.Clear(enemiesInSphere, 0, enemiesInSphere.Length);
        System.Array.Clear(enemiesInBox, 0, enemiesInBox.Length);


        Debug.Log("locked onto " + lockedOnEnemy);

    }
    //look at enemy when attacking
    public void LookLockOn()
    {
        if (lockedOnEnemy == null)
            return;
        //get the position of enemy relative to player
        Vector3 relativePosition = lockedOnEnemy.transform.position - this.transform.position;
        //this is so the character doesnt look up or down, only straight forward
        relativePosition.y = 0f;
        //rotate to face it
        this.transform.rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
    }




}
