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
        rb.gravityScale = 1f;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput < 0) sr.flipX = true;
        else if (moveInput > 0) sr.flipX = false;

        if (isInWater)
            WaterControl();
        else
            GroundControl();
    }

    void GroundControl()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void WaterControl()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.linearDamping = waterDrag;
        rb.linearVelocity = new Vector2(
            moveInput * waterSpeed,
            rb.linearVelocity.y

        );

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