using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    #region Properties
    public bool IsJumping { get; set; }
    public bool IsSuiciding { get; set; }
    public bool canBeAttacked { get; protected set; }
    #endregion

    #region Variable
    private Vector2 playerVelocity;
    private float currentGravity;

    [Header("Extra Player")]
    [SerializeField]
    private Object DefaultActionAccessor;
    private IAction defaultAction;
    private BaseConfig defaultConfig;

    #endregion

    private void Start()
    {
        defaultAction = (IAction)DefaultActionAccessor;
        currentGravity = config.gravityForce;
        GameSystem.Instance.SetAlive(this);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.right * 5, Color.blue);
        IsGrounded();
        CheckInput();

        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
            IsInAction = false;
        }

    }

    #region Mouvement

    private void Jump()
    {
        if (isGrounded)
        {
            var forceToApply = new Vector2(0, config.jumpForce);
            AddForce(forceToApply, ForceMode2D.Impulse);
        }
    }
    #endregion

    #region Input

    private void CheckInput()
    {
        if (IsJumping)
        {
            Jump();
            IsJumping = false;
        }

        if (IsInAction)
        {
            PerformAction();
        }

        if (IsSuiciding)
        {
            PerformSuicide();
        }
    }
    #endregion

    protected override void PerformSuicide()
    {
        base.PerformSuicide();
        GameSystem.Instance.KillPlayer(this);
    }

    protected override void ExtraSettingForEntityChange()
    {
        EnableEnemyDetection();
    }

    public void EnableEnemyDetection()
    {
        canBeAttacked = true;
    }
    public void DisableEnemyDetection()
    {
        canBeAttacked = false;
    }

    public void SwapToEnergyVisual()
    {

    }
}
