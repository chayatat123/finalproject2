using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    Rigidbody rb;
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            move = -1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            move = 1;

        rb.linearVelocity = new Vector3(move * speed, rb.linearVelocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}