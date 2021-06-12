using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region Customisation
    [Header("Properties")]
    [SerializeField]
    public Object shittyInterfaceAccess;
    [SerializeField]
    protected IdentityController identity;
    [SerializeField]
    protected CapsuleCollider colliderDimension;
    [SerializeField]
    protected BaseConfig config;
    [SerializeField]
    protected Rigidbody rb;
    #endregion

    #region Accesser
    public IdentityController Identity
    {
        get
        {
            return identity;
        }
    }

    public CapsuleCollider ColliderDimension
    {
        get
        {
            return colliderDimension;
        }
    }

    protected IAction actionController;
    public IAction Action
    {
        get
        {
            return actionController;
        }
    }

    public BaseConfig Config
    {
        get
        {
            return Config;
        }
    }

    #endregion

    #region Systeme
    [HideInInspector]
    public bool canMove;
    protected float direction;

    public float Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public bool IsInAction { get; set; }
    #endregion

    private void Awake()
    {
        actionController = (IAction)shittyInterfaceAccess;
        canMove = true;
    }

    #region Possession
    public virtual void StartEntityChange(BaseController targetController, float duration)
    {
        //Disable Target
        targetController.DisablEntityPhysic();

        //Position Player
        canMove = false;
        transform.position = targetController.transform.position + new Vector3(0, 1, 0);

        StartCoroutine(ChannelEntityChange(duration, targetController));
    }

    protected IEnumerator ChannelEntityChange(float duration, BaseController targetController)
    {
        yield return new WaitForSeconds(duration);
        ChangeEntity(targetController);
    }

    protected void ChangeEntity(BaseController targetController)
    {
        ChangeIdentity(targetController);
        ChangeCharacteristics(targetController);
        ChangeSkill(targetController);
        EnablentityPhysic();
        targetController.DestroyEntity();
    }

    protected void ChangeIdentity(BaseController targetController)
    {
        AdjustCollider(targetController);
        identity.runtimeAnimtor = targetController.identity.runtimeAnimtor;
        EnableEntityVisual();
        targetController.DisableEntityVisual();
    }

    protected virtual void AdjustCollider(BaseController targetController)
    {
        Debug.Log("Base Adjust");
        if (colliderDimension != null)
        {
            colliderDimension.radius = targetController.ColliderDimension.radius;
            colliderDimension.height = targetController.ColliderDimension.height;
        }
    }

    protected void ChangeCharacteristics(BaseController targetController)
    {
        config = targetController.config;
    }
    
    protected void ChangeSkill(BaseController targetController)
    {
        actionController = targetController.actionController;
    }

    #endregion

    #region Feedback
    public void OnHit()
    {
        //Default state Animator, Feedback surement ailleurs dans Identity? qui sont liés / déliés delegate?
    }

    public void OnDeath()
    {

    }

    public void OnShoot()
    {

    }

    #endregion

    public void EnablentityPhysic()
    {
        canMove = true;
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        if (colliderDimension != null)
            colliderDimension.isTrigger = false;
    }

    public void EnableEntityVisual()
    {
        if (Identity != null)
            Identity.ResetRuntimeAnimatorController();
    }

    public void DisablEntityPhysic()
    {
        canMove = false;
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        if (colliderDimension != null)
            colliderDimension.isTrigger = true;
    }

    public void DisableEntityVisual()
    {
        if (Identity?.anim != null)
            Identity.anim.runtimeAnimatorController = null;
    }

    public void DestroyEntity()
    {
        //Destroy(gameObject);
    }


}
