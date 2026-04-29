using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // ลากลูกบอลมาใส่ในช่องนี้
    public Vector3 offset = new Vector3(0, 2, -5); // ระยะห่างระหว่างกล้องกับลูกบอล
    public float smoothSpeed = 0.125f; // ความนุ่มนวลในการเคลื่อนที่ (0-1)

    void LateUpdate()
    {
        if (target != null)
        {
            // ตำแหน่งที่กล้องควรจะไป (ตำแหน่งบอล + ระยะห่าง)
            Vector3 desiredPosition = target.position + offset;

            // ทำให้การเคลื่อนที่นุ่มนวลขึ้นด้วย Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // อัปเดตตำแหน่งกล้อง
            transform.position = smoothedPosition;

            // (ทางเลือก) ให้กล้องหันมองที่ลูกบอลตลอดเวลา
            // transform.LookAt(target);
        }
    }
}