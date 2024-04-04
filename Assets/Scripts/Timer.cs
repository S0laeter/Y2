using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float maxTime;
    public float currentTime;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.OnEnemyKilled += UpdateTimer;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.OnEnemyKilled -= UpdateTimer;
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

        //update timer text
        float minutes = Mathf.Clamp(Mathf.Floor(currentTime / 60), 0f, 60f);
        float seconds = Mathf.Clamp(Mathf.Floor(currentTime % 60), 0f, 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void UpdateTimer(EnemyController enemy)
    {
        currentTime += enemy.extraTimeOnDeath;
    }

    private void TimeOut()
    {

    }

}
