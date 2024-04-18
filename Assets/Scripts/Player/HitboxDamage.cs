using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{

    private float damage;
    private float knockback;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.PassHitboxDamage += SetDamage;
        Actions.PassHitboxKnockback += SetKnockback;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassHitboxDamage -= SetDamage;
        Actions.PassHitboxKnockback -= SetKnockback;
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
