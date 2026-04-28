using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f; // ระยะที่เดินจากจุดเริ่ม

    private int direction = 1;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // จำจุดเริ่ม
    }

    void Update()
    {
        // เดิน
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // เช็คระยะจากจุดเริ่ม
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
}