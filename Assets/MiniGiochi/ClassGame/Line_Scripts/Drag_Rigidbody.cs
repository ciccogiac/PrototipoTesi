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
    private Gradient line_color;
    [SerializeField] Gradient line_connection_color;
    public LineController lc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponentInChildren<LineRenderer>();
        line_color = line.colorGradient;
        lc = GetComponentInChildren<LineController>();

        transform.Rotate(0f, 0f, 90f);
    }

    //Collega il metodo all'attributo , impostando il dragging a false , la posizione della freccia in quella desiderata e cambiando colore.

    public void connect(Vector3 connection_position)
    {
        dragging = false;
        transform.position = connection_position;
        line.colorGradient = line_connection_color ;

    }

    //Disconnette il metodo dall'attributo , basta solo il cambio colore
    public void disconnect()
    {
        line.colorGradient = line_color;
    }
    void FixedUpdate()
    {
        //Gestisce il movimento della punta della linea

        if (dragging)
        {           
            // Leggi la posizione del mouse
            Vector3 posizioneMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posizioneMouse.z = 0f; // Assicurati che la coordinata Z sia zero poiché stiamo lavorando in 2D

            // Applica la forza al Rigidbody2D per muovere l'oggetto
            rb.MovePosition(posizioneMouse);
            
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
