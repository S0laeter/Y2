using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action<PlayerController> UpdatePlayerHealthBar;
    public static Action<EnemyController> UpdateEnemyHealthBar;
    public static Action<TimerManager> UpdateTimer;
    
    public static Action<float> PassHitboxDamage;
    public static Action<float> PassHitboxKnockback;
    public static Action<float> PassProjectileDamage;
    public static Action<float> PassProjectileKnockback;

    public static Action OnAttackButtonPressed;
    public static Action OnSkillButtonPressed;
    public static Action OnDashButtonPressed;

    public static Action<GameObject> LevelSelectButtonPressed;
    
    public static Action<EnemyController> OnEnemyKilled;
    public static Action<PlayerController> OnPlayerKilled;
    public static Action OnTimeOut;
}
