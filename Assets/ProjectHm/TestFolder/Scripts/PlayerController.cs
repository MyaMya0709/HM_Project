using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : UnitBase
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Vector3 MoveDirection { get; set; } = Vector3.zero;

    [Header("Jump")]
    public float jumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsDead) return;

        MoveDirection = moveInput;
        transform.position += MoveDirection * stats.moveSpeed * Time.deltaTime;

        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //}

        //if (Input.GetMouseButtonDown(0)) // 좌클릭 공격
        //{
        //    Attack();
        //}
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * stats.moveSpeed, rb.linearVelocity.y);
    }

    private void Attack()
    {
        animator?.SetTrigger("Attack");
        // 무기공격 로직 연결 예정
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }
}
