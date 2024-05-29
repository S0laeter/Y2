using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public float autoDestroyTime;

    private float hitboxDamage;
    public string projectileHitboxType;
    public float projectileSpeed;

    private EnemyController enemyController;

    private void OnEnable()
    {
        enemyController = this.transform.parent.gameObject.GetComponent<EnemyController>();
        Destroy(this.gameObject, autoDestroyTime);
    }

    private void OnDisable()
    {

    }

    //move forward
    void FixedUpdate()
    {
        transform.position += transform.TransformDirection(Vector3.forward * projectileSpeed) * Time.deltaTime;
    }

    //when hit enemy
    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            //get damage from the enemy controller script
            hitboxDamage = enemyController.attackDamage;

            PlayerController playerController = player.GetComponent<PlayerController>();

            //if normal hit, deal damage
            //if heavy hit, deal damage and stagger
            switch (projectileHitboxType)
            {
                case "Hitbox":
                    playerController.TakeDamage(hitboxDamage);
                    break;
                case "HitboxHeavy":
                    playerController.TakeDamage(hitboxDamage);
                    playerController.TakeHeavyHit();
                    break;
                default:
                    playerController.TakeDamage(hitboxDamage);
                    break;
            }

        }
    }
}
