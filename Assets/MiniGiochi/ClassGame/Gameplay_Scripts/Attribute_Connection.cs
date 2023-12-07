using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attribute_Connection : MonoBehaviour
{
    public string attribute_name;
    public bool is_public = false;
    public string method_name;

    [SerializeField] Transform connection_point;
    [SerializeField] TextMeshProUGUI text_attribute;
    [SerializeField] TextMeshProUGUI text_visibility;

    public void changeAttributeVisibility()
    {
        if (is_public) { is_public = false; text_visibility.text = "Private"; }
        else { is_public = true; text_visibility.text = "Public"; }
    }

    private void Start()
    {
        text_attribute.text = attribute_name;
        if (is_public) { text_visibility.text = "Public"; }
        else { text_visibility.text = "Private"; }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow_Pointer"))
        {

            collision.GetComponent<Drag_Rigidbody>().connect(connection_point.position);
            method_name = collision.GetComponent<Drag_Rigidbody>().method_name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow_Pointer") && method_name == collision.GetComponent<Drag_Rigidbody>().method_name)
        {
            method_name = "";
            collision.GetComponent<Drag_Rigidbody>().disconnect();
        }
    }
}
