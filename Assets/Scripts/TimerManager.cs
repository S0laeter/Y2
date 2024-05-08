using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
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
        StartCoroutine(TimerCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            TimeOut();
        }
    }

    //timer countdown, do it this way to only update the clock once every second instead of every frame
    public IEnumerator TimerCountdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;

            Actions.UpdateTimer(this);
        }
    }

    public void UpdateTimerOnKill(EnemyController enemy)
    {
        currentTime += enemy.extraTimeOnDeath;
    }

    private void TimeOut()
    {
        Actions.OnTimeOut();
    }

}
