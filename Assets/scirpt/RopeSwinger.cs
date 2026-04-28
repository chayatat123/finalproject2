using UnityEngine;

public class RopeSwinger : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D joint;

    float cooldown = 1f;
    float lastUseTime = -Mathf.Infinity;

    void Start()
    {
        joint.enabled = false;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // กด E + มีคูลดาวน์
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastUseTime + cooldown)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                joint.enabled = true;
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, hit.point);

                lineRenderer.positionCount = 2;

                // ⏱️ เริ่มนับคูลดาวน์
                lastUseTime = Time.time;
            }
        }

        // ปล่อย E
        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
            lineRenderer.positionCount = 0;
        }

        if (joint.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, joint.connectedAnchor);
        }
    }
}