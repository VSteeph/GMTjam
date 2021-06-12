using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask Mask;
    public string targetTag;
    public float lifeTime;
    private float currentTimer;

    public void Init(Vector2 direction, float velocity, LayerMask collisionmask, string target)
    {
        Mask = collisionmask;
        targetTag = target;
        rb.velocity = direction * velocity;
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if(lifeTime< currentTimer)
        {
            Destroy(gameObject);
        }
    }
}
