using UnityEngine;
using UnityEngine.InputSystem;

public class Player : UnitBase
{
    public Vector2 moveInput;
    private PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Move.performed += OnMovePerformed;
        inputAction.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        inputAction.Player.Move.performed -= OnMovePerformed;
        inputAction.Player.Move.canceled -= OnMoveCanceled;
        inputAction.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    public void FixedUpdate()
    {
        OnMove(moveInput);
    }
}
