using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;
    public float waterSpeed = 3f;

    [Header("Jump")]
    public float jumpForce = 450f;

    [Header("Water")]
    public float waterGravity = 0.2f;
    public float surfaceJumpForce = 600f;
    public float swimUpForce = 400f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float moveInput;
    private bool isGrounded;
    private bool isInWater;
    private bool wasInWater;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput =
            (Keyboard.current.dKey.isPressed ? 1 : 0) -
            (Keyboard.current.aKey.isPressed ? 1 : 0);

        if (moveInput < 0) sr.flipX = true;
        else if (moveInput > 0) sr.flipX = false;

        if (isInWater)
            WaterControl();
        else
            GroundControl();
        if (wasInWater && !isInWater)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7f);
            }
        }

        wasInWater = isInWater;
    }

    void GroundControl()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void WaterControl()
    {
        float verticalInput = 0;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.spaceKey.isPressed)
            verticalInput = 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            verticalInput = -1;

        // ความเร็วในน้ำ (ช้าลง)
        rb.linearVelocity = new Vector2(
            moveInput * waterSpeed,
            verticalInput * waterSpeed
        );

        // จำกัดความเร็วไม่ให้พุ่งเกิน
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, waterSpeed);

        // แรงลอยนิด ๆ (ทำให้ไม่จมเร็ว)
        if (verticalInput == 0)
        {
            rb.AddForce(Vector2.up * 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
            rb.gravityScale = 0.1f;   // ลดแรงโน้มถ่วงหนัก ๆ
            rb.linearDamping = 4f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            rb.gravityScale = 1f;
            rb.linearDamping = 0f;
        }
    }
}