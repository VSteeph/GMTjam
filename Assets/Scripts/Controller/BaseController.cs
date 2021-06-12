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
    protected CapsuleCollider2D colliderDimension;
    [SerializeField]
    protected BaseConfig config;
    [SerializeField]
    protected Rigidbody2D rb;
    #endregion

    #region Accesser
    public IdentityController Identity
    {
        get
        {
            return identity;
        }
    }

    public CapsuleCollider2D ColliderDimension
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
    [SerializeField]
    protected LayerMask groundLayer;
    [HideInInspector]
    protected bool canMove;
    protected float direction;
    protected float actionTimer;
    protected bool isGrounded;
    protected bool wasGrounded;

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

    #region Movement
    protected void IsGrounded()
    {
       
        Vector3 distance = -transform.up * (colliderDimension.size.y / 2 + config.CheckgroundMargin);
        Debug.DrawRay(transform.position, distance, Color.red);
        if (Physics2D.Raycast(transform.position, -Vector2.up, (colliderDimension.size.y / 2 + config.CheckgroundMargin), groundLayer))
        {
            if (isGrounded)
            {
                OnLand();
                ResetGravity();
                wasGrounded = true;
            }             
            else
            {
                isGrounded = true;             
            }
        }
        else
        {
            if (isGrounded)
                isGrounded = false;
            else
                wasGrounded = false;
        }
    }

    protected void PerformMovement()
    {
        Vector2 currentVelocity = Vector2.zero;
        Vector2 velocity = new Vector2(Direction * config.speed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, velocity, ref currentVelocity, config.VelocitySmoothness);
        if (Direction > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Direction < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        }
    }

    protected void AdjustGravity()
    {
        if (!isGrounded)
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, Mathf.Max((Physics2D.gravity.y * config.gravityMultiplier), -config.gravityLimit));
        }
    }

    protected void ResetGravity()
    {
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, -config.gravityForce);
    }

    protected void AddForce(Vector2 force, ForceMode2D forceMode)
    {
        rb.AddForce(force, forceMode);
    }


    #endregion

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
        transform.parent = null;
        ExtraSettingForEntityChange();
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
            colliderDimension.size = targetController.ColliderDimension.size;
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

    protected virtual void ExtraSettingForEntityChange()
    {

    }


    #endregion

    #region Actions
    protected void PerformAction()
    {
        if (actionTimer == 0)
        {
            actionController?.UseAction(this, transform, config.actionDuration, config.actionRange);
        }
        else
        {
            actionTimer += Time.deltaTime;
            if (actionTimer > config.actionCooldown)
            {
                IsInAction = false;
                actionTimer = 0;
            }
        }
    }

    protected virtual void PerformSuicide()
    {
        DisablEntityPhysic();
        Identity.Death(this);
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

    public void OnLand()
    {

    }

    #endregion

    #region GameObject Management

    public void EnablentityPhysic()
    {
        canMove = true;
        if (rb != null)
        {
            rb.gravityScale = 0;
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
            rb.gravityScale = 1;
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

    #endregion


}
