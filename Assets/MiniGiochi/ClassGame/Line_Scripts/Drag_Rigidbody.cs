using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ha il compito principale di spostare la freccia della linea. Se si è sopra col mouse e si clicca si sposta altrimenti no.
    Inoltre gestisce la connessione e la discossione della linea con l'attributo , cambiando colore della linea e posizione.
 */

public class Drag_Rigidbody : MonoBehaviour
{
    public bool dragging = false;
    Rigidbody2D rb;

    public string method_name;

    private LineRenderer line;
    public Gradient line_connection_color;
    public LineController lc;

    public bool isConnected = false;

    public Transform lineConnectionPoint;

    //private GameManager_ClassGame gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponentInChildren<LineRenderer>();
        lc = GetComponentInChildren<LineController>();

        transform.Rotate(0f, 0f, 90f);
    }

    public void SetConnectionColor(){ line_connection_color = FindObjectOfType<GameManager_ClassGame>().GetLineColor();}

    //Collega il metodo all'attributo , impostando il dragging a false , la posizione della freccia in quella desiderata e cambiando colore.

    public void connect(Vector3 connection_position)
    {
        dragging = false;
        transform.position = connection_position;
        //line.colorGradient = line_connection_color ;
        isConnected = true;

    }

    //Disconnette il metodo dall'attributo , basta solo il cambio colore
    public void disconnect()
    {
        //line.colorGradient = line_color;
        isConnected = false;
    }
    void FixedUpdate()
    {
        //Gestisce il movimento della punta della linea

        if (dragging)
        {           
            // Leggi la posizione del mouse
            Vector3 posizioneMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posizioneMouse.z = 0f; // Assicurati che la coordinata Z sia zero poiché stiamo lavorando in 2D


            Transform p = lc.nodes[lc.nodes.Count - 2].transform;
            Vector2 dir =  p.position  - posizioneMouse ;
            int layerMask = LayerMask.GetMask("Obstacle");
            RaycastHit2D hit = Physics2D.Raycast(posizioneMouse, dir.normalized, dir.magnitude, layerMask);
            Debug.DrawRay(posizioneMouse, dir.normalized * dir.magnitude, Color.red, 1f);

            if (hit.collider == null)
            {
                rb.MovePosition(posizioneMouse);
            }
            
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.Sleep();
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        //stop dragging
        dragging = false;
        rb.velocity = new Vector2(0f,0f);
        rb.Sleep();
    }


}
