using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //declare wave class
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemiesToSpawn;
    }

    public int spawnerID;

    public Wave[] waves;

    private Wave currentWave;
    private int currentWaveNumber = 0;

    private int enemiesKilled = 0;
    
    private bool shouldSpawn = true;

    private void OnEnable()
    {
        currentWave = waves[0];
        SpawnWave();
        
        Actions.OnEnemyKilled += onEnemyKilled;
    }
    private void DisEnable()
    {
        Actions.OnEnemyKilled -= onEnemyKilled;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies in the wave died, increase wave number by 1 and spawn all the enemies in it
        if (enemiesKilled >= currentWave.enemiesToSpawn.Length)
        {
            //reset kill count
            enemiesKilled = 0;

            //if there's no wave left, call an event announcing it and return
            currentWaveNumber++;
            if (currentWaveNumber >= waves.Length)
            {
                Actions.SpawnerIsEmpty(spawnerID);
                return;
            }

            //if there are waves left, increase wave number by one
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
