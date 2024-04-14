using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action<PlayerController> UpdateHealthBar;

    public static Action<EnemyController> OnEnemyKilled;

    public static Action OnAttackButtonPressed;
    public static Action OnSkillButtonPressed;
    public static Action OnDashButtonPressed;
    public static Action<float> HitboxDamage;
}
