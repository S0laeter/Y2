using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public Slider hpSlider;
    public Image hpFill;
    public Gradient gradient;

    public Slider energySlider;
    public Image energyFill;

    public TextMeshProUGUI timerText;

    public GameObject winScreen;
    public GameObject loseScreen;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.UpdatePlayerHealthBar += UpdateHealthBar;
        Actions.UpdatePlayerEnergyBar += UpdateEnergyBar;
        Actions.UpdateTimer += UpdateTimer;

        Actions.Win += Win;
        Actions.Lose += Lose;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.UpdatePlayerHealthBar -= UpdateHealthBar;
        Actions.UpdatePlayerEnergyBar -= UpdateEnergyBar;
        Actions.UpdateTimer -= UpdateTimer;

        Actions.Win -= Win;
        Actions.Lose -= Lose;
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



    private void Win()
    {
        StartCoroutine(WaitForWinScreen());
    }

    private void Lose()
    {
        StartCoroutine(WaitForLoseScreen());
    }

    private IEnumerator WaitForWinScreen()
    {
        //wait for death animation
        yield return new WaitForSeconds(1f);

        winScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        sceneTransition.LoadScene("LevelSelect");
    }

    private IEnumerator WaitForLoseScreen()
    {
        //wait for death animation
        yield return new WaitForSeconds(1f);

        loseScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        sceneTransition.LoadScene("LevelSelect");
    }


}
