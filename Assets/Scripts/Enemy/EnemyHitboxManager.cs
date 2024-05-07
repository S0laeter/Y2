using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxManager : MonoBehaviour
{
    private float hitboxDamage;
    private EnemyController enemyController;

    private void Start()
    {
        //weird ass line lmao, but as long as it works
        enemyController = this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            //get damage from the enemy controller script
            hitboxDamage = enemyController.attackDamage;

            PlayerController playerController = player.GetComponent<PlayerController>();

            //if normal hit, deal damage
            //if heavy hit, deal damage and stagger
            switch (this.name)
            {
                case "Hitbox":
                    playerController.TakeDamage(hitboxDamage);
                    break;
                case "HitboxHeavy":
                    playerController.TakeDamage(hitboxDamage);
                    playerController.TakeHeavyHit();
                    break;
                default:
                    break;
            }

        }
    }

}
