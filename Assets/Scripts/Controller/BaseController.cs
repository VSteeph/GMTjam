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
    protected IdentityController visual;


    public CapsuleCollider collider;

    [Header("config")]
    public BaseConfig config;
    #endregion

    #region Systeme
    protected IAction actionController;
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
    #region Feedback
    public void OnHit()
    {
        visual.Hit();
    }

    public void OnDeath()
    {

    }

    public void OnShoot()
    {

    }

    #endregion

    #region Possession
    public virtual void ChangeIdentity(Controllable controllable)
    {
        actionController = controllable.action;
        visual.anim.runtimeAnimatorController = controllable.identity.anim.runtimeAnimatorController;
        if (collider != null)
        {
            collider.radius = controllable.identity.collider.radius;
            collider.height = controllable.identity.collider.height;
        }
    }

    #endregion
}
