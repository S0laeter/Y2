using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitboxManager : MonoBehaviour
{
    public float autoDestroyTime;

    private float damage;
    public float horizontalKnockback;
    public float verticalKnockback;

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
