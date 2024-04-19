using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitboxManager : MonoBehaviour
{
    public float autoDestroyTime;

    private float damage;
    private float knockback;

    private void OnEnable()
    {
        Destroy(this.gameObject, autoDestroyTime);

        //subscribing to actions
        Actions.PassProjectileDamage += SetDamage;
        Actions.PassProjectileKnockback += SetKnockback;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassProjectileDamage -= SetDamage;
        Actions.PassProjectileKnockback -= SetKnockback;
    }

    //receive hitbox damage and knockback
    private void SetDamage(float hitboxDamage)
    {
        damage = hitboxDamage;
    }
    private void SetKnockback(float hitboxKnockback)
    {
        knockback = hitboxKnockback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when hit enemy
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage);
            enemy.GetComponent<Rigidbody>().velocity = this.transform.forward * knockback;
        }
    }

}
