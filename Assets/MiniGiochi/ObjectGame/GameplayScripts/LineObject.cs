using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObject : MonoBehaviour
{
    public List<Transform> nodes;
    public LineRenderer lr;

    // Start is called before the first frame update

    private void OnValidate()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = nodes.Count;

    }

    public void LineSetPosition() { lr.SetPositions(nodes.ConvertAll(n => n.position).ToArray()); }
}
