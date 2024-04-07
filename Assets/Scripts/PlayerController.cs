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
    public float dashTime = 0.25f;
    public float dashCooldown = 0.5f;

    public float maxHealth = 100f;
    public float currentHealth;


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
            Die();
        }

    }

    void FixedUpdate()
    {

        //cant move while dashing
        if (isDashing)
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

        //when moving
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //rotate according to direction
            this.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        //run animation
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            anim.SetTrigger("Run");
        else
            anim.SetTrigger("Idle");

    }

    public void TakeDamage(float damage)
    {
        if (!isDashing)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

            Actions.UpdateHealthBar(this);
        }
    }

    public void Die()
    {

    }


}
