using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentityController : MonoBehaviour
{
    public Animator anim;
    public RuntimeAnimatorController runtimeAnimtor;

    private void Awake()
    {
        ResetRuntimeAnimatorController();
    }

    public void ResetRuntimeAnimatorController()
    {
        anim.runtimeAnimatorController = runtimeAnimtor;
    }
    
    public void SetNewRunTimeAnimator(RuntimeAnimatorController animatorController)
    {
        anim.runtimeAnimatorController = animatorController;
    }

    public virtual void Hit()
    {
        Debug.Log("Has been hit");
    }

    public virtual void Death(BaseController controller)
    {
        Debug.Log("Death of " + gameObject.name);
        controller.DisableEntityVisual();
    }
}
