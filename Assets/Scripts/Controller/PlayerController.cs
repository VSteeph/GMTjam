using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    #region Properties
    public bool IsJumping { get; set; }
    public bool IsSuiciding { get; set; }
    public bool isGrounded { get; private set; }
    public bool canBeAttacked { get; protected set; }
    #endregion

    #region Variable
    private Vector3 playerVelocity;
    private float currentGravity;
    private bool isFalling;

    [Header("Extra Player")]
    [SerializeField]
    private CharacterController charController;

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
        isGrounded = charController.isGrounded;
        playerVelocity.x = direction * config.speed;
        CheckInput();

    }

    #region Mouvement
    private void FixedUpdate()
    {
        if (canMove)
        {
            charController.Move(playerVelocity * Time.fixedDeltaTime);
        }
            
        if (playerVelocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (playerVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        }

        if (!isGrounded && canMove)
        {
            currentGravity = Mathf.Min((currentGravity * config.gravityMultiplier), config.gravityLimit);
            playerVelocity.y -= currentGravity;
            isFalling = true;
        }
        if (isGrounded && isFalling)
        {
            Debug.Log("landed");
            currentGravity = config.gravityForce;
            isFalling = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = config.jumpForce;
            Debug.Log("Y velocity added :" + config.jumpForce);
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

    protected override void AdjustCollider(BaseController targetController)
    {
        charController.radius = targetController.ColliderDimension.radius;
        charController.height = targetController.ColliderDimension.height;
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
