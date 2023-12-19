using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePointLine : MonoBehaviour
{
    [SerializeField] LineController lc;
    public List<LineController> lines;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line")) {
            lc = collision.GetComponent<LineController>();
            if (!lines.Contains(lc))
            {
                lines.Add(lc);

                GameObject node = new GameObject("node");
                node.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                node.transform.SetParent(this.transform);

                lc.setNodes(node.transform, this);


            }
        }
    }

    public void RemoveLine(LineController linea)
    {
        if (lines.Contains(linea))
        {
            lines.Remove(linea);
        }
    }
   
}
