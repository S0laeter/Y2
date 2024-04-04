using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;

    public float extraTimeOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0f)
        {
            Die();
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

}
