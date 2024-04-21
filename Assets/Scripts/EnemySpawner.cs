using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //declare waves class
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemiesToSpawn;
    }

    public Wave[] waves;

    private Wave currentWave;
    private int currentWaveNumber = 0;

    private int enemiesKilled = 0;
    
    private bool shouldSpawn = true;

    private void OnEnable()
    {
        Actions.OnEnemyKilled += onEnemyKilled;
    }
    private void DisEnable()
    {
        Actions.OnEnemyKilled -= onEnemyKilled;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWave = waves[0];

        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies in the wave died, increase wave number by 1 and spawn all the enemies in it
        if (enemiesKilled >= currentWave.enemiesToSpawn.Length)
        {
            //reset kill count
            enemiesKilled = 0;

            //increase wave count by one
            currentWaveNumber++;
            if (currentWaveNumber >= waves.Length)
                return;
            currentWave = waves[currentWaveNumber];

            //spawn enemies
            shouldSpawn = true;
            SpawnWave();
        }
        
    }

    public void SpawnWave()
    {
        if (shouldSpawn)
        {
            //take all the enemies in the wave and spawn them out
            for (int i = 0; i < currentWave.enemiesToSpawn.Length; i++)
            {
                Instantiate(currentWave.enemiesToSpawn[i], this.transform.position, this.transform.rotation);
            }
        }

        shouldSpawn = false;
    }

    public void onEnemyKilled(EnemyController enemy)
    {
        enemiesKilled++;
    }

}
