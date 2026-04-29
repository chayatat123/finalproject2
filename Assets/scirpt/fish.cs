using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float moveDistance = 3f;

    private int direction = 1;
    private Vector3 startPos;

    [Header("Attack")]
    public float knockbackForce = 8f;
    public float upwardForce = 5f;
    public int damage = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // ว่ายไปข้างหน้า
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        float distance = transform.position.x - startPos.x;

        if (distance >= moveDistance)
            Flip(-1);
        else if (distance <= -moveDistance)
            Flip(1);
    }

    void Flip(int newDir)
    {
        direction = newDir;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerRb != null)
        {
            // 👉 ดันออกจากปลา
            float dirX = Mathf.Sign(playerRb.position.x - transform.position.x);
            if (dirX == 0) dirX = 1;

            Vector2 force = new Vector2(dirX * knockbackForce, upwardForce);

            playerRb.linearVelocity = Vector2.zero;
            playerRb.AddForce(force, ForceMode2D.Impulse);
        }

        if (hp != null)
        {
            hp.TakeDamage(damage);
        }
    }
}