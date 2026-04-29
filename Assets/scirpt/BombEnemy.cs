using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private int direction = 1;
    private Vector3 startPos;

    [Header("Explosion")]
    public float knockbackForce = 10f;
    public float upwardForceMultiplier = 0.8f;
    public int damage = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        float distance = transform.position.x - startPos.x;

        if (distance >= moveDistance)
            Flip(-1);
        else if (distance <= -moveDistance)
            Flip(1);
    }

    void Flip(int newDirection)
    {
        direction = newDirection;

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
            // ✅ แก้ ERROR ตรงนี้ (ใช้ .x ทั้งสองฝั่ง)
            float dirX = Mathf.Sign(playerRb.position.x - transform.position.x);

            if (dirX == 0) dirX = 1;

            float forceX = dirX * knockbackForce;
            float forceY = knockbackForce * upwardForceMultiplier;

            // ถ้าเหยียบหัว → ลดแรงด้านข้าง
            float yDiff = playerRb.position.y - transform.position.y;
            if (yDiff > 0.5f)
            {
                forceX *= 0.4f;
            }

            Vector2 force = new Vector2(forceX, forceY);

            // เคลียร์แรงก่อน
            playerRb.linearVelocity = Vector2.zero;

            // ใส่แรง
            playerRb.AddForce(force, ForceMode2D.Impulse);
        }

        // ลดเลือด
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        // ลบศัตรู
        Destroy(gameObject);
    }
}