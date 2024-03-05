using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController))]
public class LineController : MonoBehaviour
{
    public List<Transform> nodes;
    public LineRenderer lr;

    public  float last_m=0;


    public  List<CreatePointLine> PointLines;
    private int point_counter = 0;

    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip connectLineAudio;
    [SerializeField] AudioClip discconnectLineAudio;


    public void setLineStartingPoint(Transform pi, Transform pf)
    {
        lr = GetComponent<LineRenderer>();
        nodes = new List<Transform>();

        nodes.Insert(0, pi);
        nodes.Insert(1, pf);
        lr.positionCount = 2;

        Color r  = GetComponentInParent<Drag_Rigidbody>().line_connection_color.colorKeys[0].color;

        Gradient gradient = new Gradient();

        // Imposta i colori per il gradiente
        gradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(r, 0f),
            new GradientColorKey(r, 0.9f),
            new GradientColorKey(Color.gray, 0.95f),
            new GradientColorKey(Color.white, 1f)
        };

        // Imposta i valori alpha per il gradiente
        gradient.alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 1f)
        };

        // Assegna il gradiente al LineRenderer
        lr.colorGradient = gradient;



    }

    public void setNodes(Transform t,CreatePointLine pl_)
    {

        nodes.Insert(nodes.Count-1,t);
        lr.positionCount= lr.positionCount + 1;
        Transform p1 = nodes[nodes.Count - 3].transform;
        Transform p2 = nodes[nodes.Count - 2].transform;
        last_m = calculate_m(p1.position,p2.position);

        point_counter++;
        PointLines.Insert(point_counter - 1, pl_);

        audio.clip = connectLineAudio;
        audio.Play();
    }

    private bool canDeleteNodes()
    {

        float offset = 0.35f;
        Transform pm2 = nodes[nodes.Count - 2].transform;
        Transform pm1 = nodes[nodes.Count - 1].transform;
        if (pm1.position.x < (pm2.position.x-offset))
        {

            Transform p1 = nodes[nodes.Count - 3].transform;
            Transform p2 = nodes[nodes.Count - 1].transform;
            Vector2 dir = p1.position - p2.position;
            int layerMask = LayerMask.GetMask("Obstacle");
            RaycastHit2D hit = Physics2D.Raycast(p2.position, dir.normalized, dir.magnitude, layerMask);
            Debug.DrawRay(p2.position, dir.normalized * dir.magnitude, Color.red, 5f);

            if (hit.collider != null)
            {
                return false;
            }

            return true;
        }

        return false;

    }

    private void deleteNodes()
    {
        if (canDeleteNodes()) {
            Transform t = nodes[nodes.Count - 2];
            nodes.RemoveAt(nodes.Count - 2);
            Destroy(t.gameObject);
            lr.positionCount = lr.positionCount - 1;



            PointLines[point_counter - 1].RemoveLine(this);
            PointLines.RemoveAt(point_counter - 1);
            point_counter--;

            if (nodes.Count > 2)
            {
                Transform p1 = nodes[nodes.Count - 3].transform;
                Transform p2 = nodes[nodes.Count - 2].transform;
                last_m = calculate_m(p1.position, p2.position);
            }

            audio.clip = discconnectLineAudio;
            audio.Play();
        }
    }

    public bool is_changing_m(Vector3 p2)
    {
        Transform p1 = nodes[nodes.Count - 2].transform;

        if (nodes.Count > 2) { 
        if ((last_m > 0 && calculate_m(p1.position,p2) < 0) || (last_m < 0 && calculate_m(p1.position,p2) > 0) )
        {
                Debug.Log("changinge m");
                return true;
                
        }
        }
        return false;
    }

    private float calculate_m(Vector3 p1, Vector3 p2){ return ((p2.y - p1.y) / (p2.x - p1.x));}

    // Update is called once per frame
    void Update()
    {
        lr.SetPositions(nodes.ConvertAll(n => n.position).ToArray());
        if(nodes.Count>2  ) { 
           // if (Input.GetMouseButtonDown(1))
                deleteNodes();
        }
    }


    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lr.startWidth;
    }
}
