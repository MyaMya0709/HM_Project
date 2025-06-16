using UnityEngine;
using UnityEngine.InputSystem;

public class Player : UnitBase
{
    public Vector2 moveInput;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
    }
    public void FixedUpdate()
    {
        OnMove(moveInput);
    }
}
