using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public Slider hpSlider;
    public Image hpFill;
    public Gradient gradient;

    public Slider energySlider;
    public Image energyFill;

    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.UpdatePlayerHealthBar += UpdateHealthBar;
        Actions.UpdatePlayerEnergyBar += UpdateEnergyBar;
        Actions.UpdateTimer += UpdateTimer;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.UpdatePlayerHealthBar -= UpdateHealthBar;
        Actions.UpdatePlayerEnergyBar -= UpdateEnergyBar;
        Actions.UpdateTimer -= UpdateTimer;
    }

    public void UpdateHealthBar(PlayerController player)
    {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;

        hpFill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }

    public void UpdateEnergyBar(PlayerController player)
    {
        energySlider.maxValue = player.maxEnergy;
        energySlider.value = player.currentEnergy;
    }

    public void UpdateTimer(TimerManager timerManager)
    {
        //change color when near time out
        if (timerManager.currentTime <= 30f)
        {
            timerText.color = Color.red;
        }

        //update timer text, in minutes and seconds
        float minutes = Mathf.Clamp(Mathf.Floor(timerManager.currentTime / 60), 0f, 60f);
        float seconds = Mathf.Clamp(Mathf.Floor(timerManager.currentTime % 60), 0f, 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
