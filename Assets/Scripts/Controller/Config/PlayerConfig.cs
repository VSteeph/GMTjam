using System;
using UnityEngine;
[Serializable]
public struct PlayerConfig
{
    [Header("System")]
    public float gravityForce;
    public float gravityMultiplier;
    public float gravityLimit;

    [Header("Mouvement")]
    public float speed;
    public float jumpForce;

}
