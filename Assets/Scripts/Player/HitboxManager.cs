using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    private float damage;
    private float knockback;

    public string hitboxType;

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
    private void SetKnockback(float hitboxHorizontalKnockback)
    {
        knockback = hitboxHorizontalKnockback;
    }

    //when hit enemy
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            //damage and knockback
            enemyController.TakeDamage(damage, hitboxType);
            enemyController.StartCoroutine(enemyController.TakeKnockback(knockback));

        }
    }

}
