using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // ลากลูกบอลมาใส่
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}