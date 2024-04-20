using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public float maxTime;
    public float currentTime;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.OnEnemyKilled += UpdateTimerOnKill;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.OnEnemyKilled -= UpdateTimerOnKill;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            TimeOut();
        }

        //countdown
        currentTime -= Time.deltaTime;

        Actions.UpdateTimer(this);
    }

    public void UpdateTimerOnKill(GameObject enemy)
    {
        currentTime += enemy.GetComponent<EnemyController>().extraTimeOnDeath;
    }

    private void TimeOut()
    {
        Actions.OnTimeOut(this);
    }

}
