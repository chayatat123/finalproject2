using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private int direction = 1;
    private Vector3 startPos;
    private Animator anim;


    [Header("Explosion")]
    public float knockbackForce = 10f;
    public int damage = 1;

    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        float distance = transform.position.x - startPos.x;

        if (distance >= moveDistance)
        {
            Flip(-1);
        }
        else if (distance <= -moveDistance)
        {
            Flip(1);
        }
    }

    void Flip(int newDirection)
    {
        direction = newDirection;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    // 💥 ชนแล้วระเบิด
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // กระเด็น
                Vector2 forceDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(forceDir * knockbackForce, ForceMode2D.Impulse);
            }

            // ลดเลือด
            PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            // ลบตัวเอง (ระเบิด)
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Player"))
            {
                anim.SetTrigger("Explode");

                // ทำดาเมจ
                collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(1);

                // ทำลายตัวเองหลังระเบิด
                Destroy(gameObject, 0.5f);
            }
        }
    }
}