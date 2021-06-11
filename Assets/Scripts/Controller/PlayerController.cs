using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    #region Properties
    public bool IsJumping { get; set; }
    public bool IsSuiciding { get; set; }
    public bool isGrounded { get; private set; }
    #endregion

    #region Variable
    private Vector3 playerVelocity;
    private float currentGravity;
    private bool isFalling;
    private float actionTimer;


    [SerializeField]
    private CharacterController charController;
    [SerializeField]
    private PlayerConfig pConfig;
    [SerializeField]
    private IAction actionController;

    private IAction defaultAction;
    private BaseConfig defaultConfig;

    #endregion

    private void Start()
    {
        currentGravity = config.gravityForce;
    }

    private void Update()
    {
        isGrounded = charController.isGrounded;
        playerVelocity.x = direction;
        CheckInput();

    }

    #region Mouvement
    private void FixedUpdate()
    {
        charController.Move(playerVelocity * config.speed * Time.fixedDeltaTime);

        if (!isGrounded)
        {
            Debug.Log("Current Gravity :" + currentGravity);
            currentGravity = Mathf.Min((currentGravity * config.gravityMultiplier), config.gravityLimit);
            playerVelocity.y -= currentGravity;
            isFalling = true;
        }
        if(isGrounded && isFalling)
        {
            Debug.Log("landed");
            currentGravity = config.gravityForce;
            isFalling = false;
        }
    }

    private void Jump()
    {
        if(isGrounded)
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

        if(IsInAction)
        {
            Action();
        }

        if(IsSuiciding)
        {
            Suicide();
        }
    }
    private void Action()
    {
        if(actionTimer == 0)
        {
            //actionController?.UseAction();
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

    private void Suicide()
    {
        
    }
    #endregion
}
