using UnityEngine;

public class WindLeftZone : MonoBehaviour
{
    public float pushForce = 15f; // แรงลม

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb == null) return;

            // พัดไปทางซ้าย
            rb.AddForce(Vector2.left * pushForce);
        }
    }
}