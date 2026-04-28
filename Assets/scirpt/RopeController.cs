using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{

    public GameObject ropeShooter;

    public LayerMask mask;

    private SpringJoint2D rope;
    public float maxRopeTime = 100f;
    private float ropeTime;

    public LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    private void LateUpdate()
    {
        if (rope != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ropeShooter.transform.position);
            lineRenderer.SetPosition(1, rope.connectedAnchor);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (rope != null)
        {
            ropeTime++;

            if (ropeTime > maxRopeTime)
            {
                GameObject.DestroyImmediate(rope);
                ropeTime = 0;
            }
        }
    }

    private void Fire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = ropeShooter.transform.position;
        Vector3 direction = mousePos - pos;

        RaycastHit2D hit = Physics2D.Raycast(pos, direction, Mathf.Infinity, mask);

        if (hit.collider != null)
        {
            SpringJoint2D newRope = ropeShooter.AddComponent<SpringJoint2D>();
            newRope.enableCollision = false;
            newRope.frequency = 1f;
            newRope.connectedAnchor = hit.point;
            newRope.enabled = true;

            GameObject.DestroyImmediate(rope);
            rope = newRope;
            ropeTime = 0;

        }
    }
}