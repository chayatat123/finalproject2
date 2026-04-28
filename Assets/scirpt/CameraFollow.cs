using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;     // ลากลูกบอลมาใส่ตรงนี้
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            ball.position.x,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}