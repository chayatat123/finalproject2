using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    public float moveHeight = 2f;   // ระยะขึ้นลง
    public float speed = 2f;        // ความเร็ว

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * moveHeight;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}