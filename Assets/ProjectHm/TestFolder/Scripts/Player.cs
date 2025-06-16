using UnityEngine;
using UnityEngine.InputSystem;

public class Player : UnitBase
{
    public Vector2 moveInput;

    private PlayerInput playerInput;

    //private void Awake()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //    playerInput.actions["Move"].performed += OnMove;
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Move(moveInput);
    }
    //public void OnMove(Vector2 direction)
    //{
    //    moveInput = direction;
    //    Move(moveInput);
    //}

    public void FixedUpdate()
    {
        
    }
}
