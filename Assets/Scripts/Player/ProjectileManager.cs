using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitboxManager : MonoBehaviour
{
    public float autoDestroyTime;

    private float damage;
    private float horizontalKnockback;
    private float verticalKnockback;

    private void OnEnable()
    {
        Destroy(this.gameObject, autoDestroyTime);

        //subscribing to actions
        Actions.PassProjectileDamage += SetDamage;
        Actions.PassProjectileHorizontalKnockback += SetHorizontalKnockback;
        Actions.PassProjectileVerticalKnockback += SetVerticalKnockback;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassProjectileDamage -= SetDamage;
        Actions.PassProjectileHorizontalKnockback -= SetHorizontalKnockback;
        Actions.PassProjectileVerticalKnockback += SetVerticalKnockback;
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

    // Update is called once per frame
    void Update()
    {

    }

    //when hit enemy
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.tag == "Enemy")
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            //damage enemy
            enemyController.TakeDamage(damage);

            //knockback enemy
            enemyController.StartCoroutine(enemyController.TakeKnockback(horizontalKnockback, verticalKnockback));
        }
    }

}
