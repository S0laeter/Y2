using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rb;

    public FixedJoystick joystick;
    private Vector2 joystickRotation;
    
    public float moveSpeed;

    public bool isAttacking;

    public bool canDash = true;
    public bool isDashing;

    public float maxHealth = 100f;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;

        Actions.UpdateHealthBar(this);

    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0f)
        {
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
        joystickRotation = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        //move according to that rotation
        rb.velocity = new Vector3( joystickRotation.x * moveSpeed, rb.velocity.y, joystickRotation.y * moveSpeed);

        //when moving or not
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //rotate according to moving direction
            this.transform.rotation = Quaternion.LookRotation(rb.velocity);

            anim.SetTrigger("Run");
        }
        else
        {
            anim.SetTrigger("Idle");
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

}
