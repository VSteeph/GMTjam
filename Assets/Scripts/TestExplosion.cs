using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplosion : MonoBehaviour
{
    public bool StartExplosion;
    public bool hasExploded;
    public float Force;
    public float Radius;
    public Vector3 PositionOffset;
    public Rigidbody2D rb;


    private void Update()
    {
        if(StartExplosion)
        {
            GameSystem.Instance.TriggerExplosion(transform.position + PositionOffset, Radius, Force);
            StartExplosion = false;
        }
    }
}
