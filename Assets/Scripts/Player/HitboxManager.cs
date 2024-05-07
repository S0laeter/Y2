using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    private float damage;
    private float horizontalKnockback;
    private float verticalKnockback = 10000f;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.PassHitboxDamage += SetDamage;
        Actions.PassHitboxHorizontalKnockback += SetHorizontalKnockback;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassHitboxDamage -= SetDamage;
        Actions.PassHitboxHorizontalKnockback -= SetHorizontalKnockback;
    }

    //receive hitbox damage and knockback
    private void SetDamage(float hitboxDamage)
    {
        damage = hitboxDamage;
    }
    private void SetHorizontalKnockback(float hitboxHorizontalKnockback)
    {
        horizontalKnockback = hitboxHorizontalKnockback;
    }

    //when hit enemy
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            //if normal hit, deal damage
            //if launch hit, deal damage and stagger
            switch (this.name)
            {
                case "Hitbox":
                    enemyController.TakeDamage(damage);
                    enemyController.StartCoroutine(enemyController.TakeKnockback(horizontalKnockback, 0));
                    break;
                case "HitboxLaunch":
                    enemyController.TakeDamage(damage);
                    enemyController.StartCoroutine(enemyController.TakeKnockback(horizontalKnockback, verticalKnockback));
                    break;
                default:
                    break;
            }
            
        }
    }

}
