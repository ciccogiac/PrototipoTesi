using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController), typeof(PolygonCollider2D))]

public class LineCollision : MonoBehaviour
{
    LineController lc;
    PolygonCollider2D polygonCollider2D;
    List<Vector2> colliderPoints = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        lc = GetComponent<LineController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        colliderPoints = CalculateColliderPoints();
        polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (colliderPoints != null) colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }

    private List<Vector2> CalculateColliderPoints()
    {
        //get all positions of the line renderer
        Vector3[] positions = lc.GetPositions();
        int p0 = positions.Length -2;
        int p1 = positions.Length - 1;

        // get the width of the line
        float width = lc.GetWidth();

        float m = (positions[p1].y - positions[p0].y) / (positions[p1].x - positions[p0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>
        {
            positions[p0] + offsets[0],
            positions[p1] + offsets[0],
            positions[p1] + offsets[1],
            positions[p0] + offsets[1]
        };

        return colliderPositions;
    }


}
