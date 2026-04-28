using UnityEngine;

public class RopeSwinger : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D joint;
    public float step = 0.02f; // ความเร็วในการขยับเชือก

    void Start()
    {
        joint.enabled = false;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // เมื่อกดคลิกเมาส์ซ้าย
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ยิง Raycast ไปหาจุดเกาะ (อาจจะใส่ LayerMask เพื่อให้เกาะได้เฉพาะบาง Object)
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // ตั้งค่าจุดเชื่อมต่อของ Joint
                joint.enabled = true;
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, hit.point);

                // ตั้งค่า Line Renderer
                lineRenderer.positionCount = 2;
            }
        }

        // เมื่อปล่อยเมาส์
        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            lineRenderer.positionCount = 0;
        }

        // อัปเดตตำแหน่งเส้นเชือกให้ตามตัวละครตลอดเวลา
        if (joint.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, joint.connectedAnchor);
        }
    }
}