using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{

    private float damage;
    private float knockback = 3f;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.HitboxDamage += SetDamage;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.HitboxDamage -= SetDamage;
    }

    private void SetDamage(float hitboxDamage)
    {
        damage = hitboxDamage;
    }

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage);
            enemy.GetComponent<Rigidbody>().velocity = transform.forward * knockback;
        }
    }

}
