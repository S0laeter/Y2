using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public Slider hpSlider;
    public Image hpFill;

    public Gradient gradient;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.UpdateHealthBar += UpdateHealthBar;
    }
    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.UpdateHealthBar -= UpdateHealthBar;
    }

    public void UpdateHealthBar(PlayerController player)
    {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;

        hpFill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }

    public void AttackButtonPressed()
    {
        Actions.OnAttackButtonPressed();
    }
    public void SkillButtonPressed()
    {
        Actions.OnSkillButtonPressed();
    }
    public void DashButtonPressed()
    {
        Actions.OnDashButtonPressed();
    }


}
