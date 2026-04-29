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
    public float swimForce = 8f;   // แรงว่ายขึ้น/ลง
    public float waterDrag = 2f;   // ความหนืดตอนอยู่ในน้ำ

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float moveInput;
    private bool isGrounded;
    private bool isInWater;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // รับ input ซ้าย-ขวา
        moveInput =
            (Keyboard.current.dKey.isPressed ? 1 : 0) -
            (Keyboard.current.aKey.isPressed ? 1 : 0);

        // หันตัวละคร
        if (moveInput < 0) sr.flipX = true;
        else if (moveInput > 0) sr.flipX = false;

        // เลือกโหมด
        if (isInWater)
            WaterControl();
        else
            GroundControl();
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

        if (Keyboard.current.sKey.isPressed)
            verticalInput = -1;

        // ✅ คุมแค่แกน X (Y ให้ Buoyancy จัดการ)
        rb.linearVelocity = new Vector2(
            moveInput * waterSpeed,
            rb.linearVelocity.y
        );

        // ✅ ใช้แรงแทนการเซ็ตค่า
        if (verticalInput != 0)
        {
            rb.AddForce(Vector2.up * verticalInput * swimForce);
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

            // ให้ Effector คุมการลอย
            rb.gravityScale = 1f;

            // เพิ่มความหนืด
            rb.linearDamping = waterDrag;
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