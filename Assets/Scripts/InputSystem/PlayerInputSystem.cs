using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    public void OnMove(InputAction.CallbackContext context)
    {
        controller.Direction = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        controller.IsJumping = context.action.triggered;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        controller.IsInAction = context.action.triggered;
    }

    public void OnSuicide(InputAction.CallbackContext context)
    {
        controller.IsSuiciding = context.action.triggered;
    }

}
