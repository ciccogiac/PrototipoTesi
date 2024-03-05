using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePointLine : MonoBehaviour
{
    [SerializeField] LineController lc;
    public List<LineController> lines;

    [SerializeField] ParticleSystem particle;

    [SerializeField] Color particleConnectColor;
    [SerializeField] Color particleDisconnectColor;
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

                particle.gameObject.transform.localScale = new Vector3(1, 1, 1);
                particle.startColor = particleConnectColor;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            lc = collision.GetComponent<LineController>();
            if (!lines.Contains(lc))
            {
                
                lines.Add(lc);

                GameObject node = new GameObject("node");
                node.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                node.transform.SetParent(this.transform);

                lc.setNodes(node.transform, this);

                particle.gameObject.transform.localScale = new Vector3(1,1,1);
                particle.startColor = particleConnectColor;
            }
        }
    }

    public void RemoveLine(LineController linea)
    {
        if (lines.Contains(linea))
        {
            lines.Remove(linea);

            if (lines.Count <= 0)
            {
                particle.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                particle.startColor = particleDisconnectColor;
            }
        }
    }
   
}
