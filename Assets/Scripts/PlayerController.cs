using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public FixedJoystick joystick;
    private Vector2 joystickRotation;
    
    public float moveSpeed;

    private bool canDash = true;
    public bool isDashing;
    public float dashPower = 24f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    public float maxHealth = 100f;
    public float currentHealth;



    private void OnEnable()
    {
        //subscribing to actions
        Actions.OnDashButtonPressed += TriggerDash;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.OnDashButtonPressed -= TriggerDash;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

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

    }

    private void TriggerDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        //dash
        rb.velocity = transform.forward * dashPower;
        
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
            
    }
}
