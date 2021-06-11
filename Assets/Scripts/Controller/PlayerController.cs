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


    [SerializeField]
    private CharacterController charController;

    [SerializeField]
    private PlayerConfig config;

    #endregion

    private void Start()
    {
        currentGravity = config.gravityForce;
    }

    private void Update()
    {
        isGrounded = charController.isGrounded;
        playerVelocity.x = direction;
        
        if(IsJumping)
        {
            Jump();
            IsJumping = false;
        }
    }

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
}
