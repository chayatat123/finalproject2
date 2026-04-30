using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    public AudioClip jumpSound;
    public AudioClip walkSound;

    private Rigidbody2D rb;

    private AudioSource walkAudio; // เสียงเดิน
    private AudioSource jumpAudio; // เสียงกระโดด

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // เพิ่ม AudioSource 2 ตัว
        AudioSource[] sources = GetComponents<AudioSource>();

        walkAudio = sources[0];
        jumpAudio = sources[1];

        walkAudio.clip = walkSound;
        walkAudio.loop = true;
    }

    void Update()
    {
        float moveX = 0;

        if (Input.GetKey(KeyCode.A))
            moveX = -1;
        if (Input.GetKey(KeyCode.D))
            moveX = 1;

        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // 🎧 เสียงเดิน
        if (moveX != 0 && isGrounded)
        {
            if (!walkAudio.isPlaying)
                walkAudio.Play();
        }
        else
        {
            if (walkAudio.isPlaying)
                walkAudio.Stop();
        }

        // 🦘 กระโดด
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpAudio.PlayOneShot(jumpSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}