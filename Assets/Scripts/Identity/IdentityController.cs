using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityController : MonoBehaviour
{
    public CapsuleCollider collider;
    public Animator anim;
    public SpriteRenderer sprite;

    public virtual void Hit()
    {
        Debug.Log("Has been hit");
    }
}
