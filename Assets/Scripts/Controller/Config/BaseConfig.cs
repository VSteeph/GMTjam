using System;
using UnityEngine;

[Serializable]
public struct BaseConfig
{
    [Header("System")]
    public float gravityForce;
    public float gravityMultiplier;
    public float gravityLimit;
    public float CheckgroundMargin;
    public float VelocitySmoothness;

    [Header("Mouvement")]
    public float speed;
    public float jumpForce;

    [Header("Skill")]
    public float actionCooldown;
    public float actionRange;
    public float actionDuration;
    public float MaxRange;
    public float MinRange;

    [Header("Projectile")]
    public float bulletVelocity;
}
