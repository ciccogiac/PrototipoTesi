using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/*gstisce la connessione di un attributo con un metodo, e permette di cambiare la visibilità di un attributo.
 */
public class Attribute_Connection : MonoBehaviour
{
    public string attribute_name;   //Attributo da collegare
    public bool is_public = false;  //Visibilità attributo
    public List<string> method_names;      //Metodi collegato
    public List<Drag_Rigidbody> method_lines;

    [SerializeField] Transform connection_point;    //Punto in cui fare avvenire la connessione tra box attributo e freccia metodo
    //Variabili testo di nomeAttributo e visibilità
    [SerializeField] TextMeshProUGUI text_attribute;
    [SerializeField] TextMeshProUGUI text_visibility;

    [SerializeField] RectTransform attribute_boxImage;

    public void changeAttributeVisibility()
    {
        if (is_public) { is_public = false; text_visibility.text = "Private"; }
        else { is_public = true; text_visibility.text = "Public"; }
    }

    private void Start()
    {
        method_names = new List<string>();
        method_lines = new List<Drag_Rigidbody>();

        text_attribute.text = attribute_name;
        if (is_public) { text_visibility.text = "Public"; }
        else { text_visibility.text = "Private"; }
        
    }

    private void Redefine_ConnectionPoints()
    {
        float f = attribute_boxImage.rect.height / (method_lines.Count + 1);
        int i = 0;

        foreach (var line in method_lines)
        {
            i++;
            Vector3 pos = connection_point.position;
            pos.y = connection_point.position.y - (f * i);
            line.connect(pos);
        }
    }

    //Alla collisione del trigger dell'attributo con la freccia metodo , connette il metodo richiamando la funzione connnect e aggiorna il method_name della connessione.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow_Pointer"))
        {
            Drag_Rigidbody method_line = collision.GetComponent<Drag_Rigidbody>();
            if (!method_names.Contains(method_line.method_name))
            {
                method_names.Add(method_line.method_name);
                method_lines.Add(method_line);
                //method_line.connect(connection_point.position);
                Redefine_ConnectionPoints();
            }
            
        }
    }

    //Quando una freccia metodo esce , si azzera la string metodo e si chiama la funzione disconnect sul metodo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow_Pointer") && method_lines.Contains(collision.GetComponent<Drag_Rigidbody>()) && method_names.Contains(collision.GetComponent<Drag_Rigidbody>().method_name))
        {
            Drag_Rigidbody method_line = collision.GetComponent<Drag_Rigidbody>();
            method_names.Remove(method_line.method_name);
            method_lines.Remove(method_line);
            method_line.disconnect();
            Redefine_ConnectionPoints();
        }
    }
}
