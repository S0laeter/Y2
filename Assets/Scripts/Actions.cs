using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action<PlayerController> UpdateHealthBar;
    
    public static Action<float> PassHitboxDamage;
    public static Action<float> PassHitboxKnockback;

    public static Action OnAttackButtonPressed;
    public static Action OnSkillButtonPressed;
    public static Action OnDashButtonPressed;

    public static Action<string> LevelSelectButtonPressed;
    
    public static Action<EnemyController> OnEnemyKilled;
}
