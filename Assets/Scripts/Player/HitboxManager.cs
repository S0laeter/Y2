using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{

    private float damage;
    private float horizontalKnockback;
    private float verticalKnockback;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.PassHitboxDamage += SetDamage;
        Actions.PassHitboxHorizontalKnockback += SetHorizontalKnockback;
        Actions.PassHitboxVerticalKnockback += SetVerticalKnockback;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassHitboxDamage -= SetDamage;
        Actions.PassHitboxHorizontalKnockback -= SetHorizontalKnockback;
        Actions.PassHitboxVerticalKnockback -= SetVerticalKnockback;
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
    private void SetVerticalKnockback(float hitboxVerticalKnockback)
    {
        verticalKnockback = hitboxVerticalKnockback;
    }

    //when hit enemy
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {

            //damage enemy
            enemy.GetComponent<EnemyController>().TakeDamage(damage);

            //knockback enemy
            enemy.GetComponent<Rigidbody>().velocity = this.transform.forward * horizontalKnockback;

        }
    }

}
