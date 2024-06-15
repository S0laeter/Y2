using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action<PlayerController> UpdatePlayerHealthBar;
    public static Action<PlayerController> UpdatePlayerEnergyBar;
    public static Action<EnemyController> UpdateEnemyHealthBar;
    public static Action<TimerManager> UpdateTimer;
    
    public static Action<float> PassHitboxDamage;
    public static Action<float> PassHitboxKnockback;
    public static Action<float> PassProjectileDamage;

    public static Action OnAttackInput;
    public static Action OnSkillInput;
    public static Action OnDashInput;
    public static Action OnUltInput;

    public static Action<GameObject> LevelSelectButtonPressed;

    public static Action<int> SpawnerIsEmpty;
    
    public static Action<EnemyController> OnEnemyKilled;
    public static Action<float> OnEnemyDamaged;
    public static Action<PlayerController> OnPlayerKilled;
    public static Action OnTimeOut;
}
