using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public float autoDestroyTime;

    private float damage;
    public float knockback;

    public string hitboxType;

    private void OnEnable()
    {
        Destroy(this.gameObject, autoDestroyTime);

        //subscribing to actions
        Actions.PassProjectileDamage += SetDamage;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.PassProjectileDamage -= SetDamage;
    }

    //receive hitbox damage and knockback
    private void SetDamage(float hitboxDamage)
    {
        damage = hitboxDamage;
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
