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
    
    public void setLineStartingPoint(Transform pi, Transform pf)
    {
        lr = GetComponent<LineRenderer>();
        nodes = new List<Transform>();

        nodes.Insert(0, pi);
        nodes.Insert(1, pf);
        lr.positionCount = 2;
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
    }

    private bool canDeleteNodes()
    {
        /*
        Transform pm3 = nodes[nodes.Count - 3].transform;
        Transform pm2 = nodes[nodes.Count - 2].transform;
        Transform pm1 = nodes[nodes.Count - 1].transform;
        float m = calculate_m(pm2.position, pm1.position);

        Vector2 dir_m = pm2.position - pm1.position;
        //Debug.Log("m dir = "+dir_m.normalized);
       // Debug.Log("m= "+ m + " last_m= " + last_m + "  dir_x= " + dir_m.x + "  dir_y= " + dir_m.y);
        float offset = 0.1f;

        
        switch (PointLines[point_counter - 1].collider_pos)
        {
            case CreatePointLine.position_collider.UP_sx:
                if (pm3.position.y == pm2.position.y)
                {
                    if (dir_m.x < 0)
                    {

                        if (m > last_m + offset) { break; }     //ok
                    }
                    else
                    {
                        if (m < last_m - offset) { break; } // on reverse
                    }
                }
                else
                {
                    if (dir_m.y < 0) // vado verso sopra
                    {

                        if (m > last_m + offset) { break; }     //ok
                    }
                    else // vado verso sotto
                    {
                        if (m < last_m - offset) { break; } // on reverse
                    }
                }
                return false;

            case CreatePointLine.position_collider.UP_dx:
                if (pm3.position.y == pm2.position.y)
                {
                    if (dir_m.x < 0)
                    {

                        if (m > last_m + offset) { break; }
                    }
                    else
                    {
                        if (m < last_m - offset) { break; } // on reverse
                    }
                }
                else
                {
                    if (dir_m.y < 0)
                    {

                        if (m > last_m + offset) { break; }
                    }
                    else
                    {
                        if (m < last_m - offset) { break; } // on reverse
                    }

                }
                return false;


            case CreatePointLine.position_collider.DW_sx:
                if (pm3.position.y == pm2.position.y)
                {
                    if (dir_m.x < 0)
                    {
                        if (m < last_m - offset) { break; }
                    }
                    else
                    {
                        if (m > last_m + offset) { break; } // on reverse
                    }
                }
                else
                {
                    if (dir_m.y < 0)
                    {
                        if (m < last_m - offset) { break; }
                    }
                    else
                    {
                        if (m > last_m + offset) { break; } // on reverse
                    }
                }
                return false;

            case CreatePointLine.position_collider.DW_dx:
                if (pm3.position.y == pm2.position.y)
                {
                    if (dir_m.x < 0)
                    {
                        if (m < last_m - offset) { break; }
                    }
                    else
                    {
                        if (m > last_m + offset) { break; } // on reverse
                    }
                }
                else
                {
                    if (dir_m.y < 0)
                    {
                        if (m < last_m - offset) { break; }
                    }
                    else
                    {
                        if (m > last_m + offset) { break; } // on reverse
                    }
                }
                 return false;

         
        }
        
        */

        float offset = 0.2f;
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
