using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region Customisation
    [SerializeField]
    protected IAction actionController;
    protected IdentityController visual;
    public BaseConfig config;
    public CapsuleCollider collider;
    #endregion

    #region Systeme
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
    public void ChangeIdentity(Controllable controllable)
    {
        actionController = controllable.action;
        visual = controllable.identity;
        collider.radius = visual.collider.radius;
        collider.height = visual.collider.height;
    }

    #endregion
}
