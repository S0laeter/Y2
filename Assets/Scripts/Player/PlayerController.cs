using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rb;

    public Transform cam;
    public FixedJoystick joystick;

    private Vector2 joystickDirection;
    
    public float moveSpeed;
    public bool isRunning;

    public bool isAttacking;

    public bool canDash = true;
    public bool isDashing;

    public float maxHealth = 100f;
    public float currentHealth;

    float turnSmoothVelocity;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;

        Actions.UpdateHealthBar(this);

    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0f)
        {
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
            anim.SetTrigger("Run");

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
            anim.SetTrigger("Idle");

            isRunning = false;

        }

    }

    //take damage, but invincible while dashing
    public void TakeDamage(float damage)
    {
        if (!isDashing)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

            Actions.UpdateHealthBar(this);
        }
    }

    //die
    public void Die()
    {
        anim.SetTrigger("Die");
        Actions.OnPlayerKilled(this.gameObject);

        //destroy player, do this last
        Destroy(this.gameObject);
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






    //simple lock on to nearest enemy when attacking, called from attack states
    public void SimpleLockOn()
    {
        //array of colliders in range, in layer Enemy
        Collider[] enemiesInRange = new Collider[100];
        enemiesInRange = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Enemy"));
        if (enemiesInRange.Length == 0)
            return;

        //sort by distance using Linq
        enemiesInRange = enemiesInRange.OrderBy((enemy) => (enemy.transform.position - transform.position).sqrMagnitude).ToArray();

        //get the direction of enemy
        Vector3 relativePosition = enemiesInRange[0].transform.position - this.transform.position;
        //this is so the character doesnt look up or down, only straight forward
        relativePosition.y = 0f;
        //rotate to face it
        this.transform.rotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        Debug.Log("locked onto " + enemiesInRange[0]);

        //clear array for next lock on
        System.Array.Clear(enemiesInRange, 0, enemiesInRange.Length);
    }

}
