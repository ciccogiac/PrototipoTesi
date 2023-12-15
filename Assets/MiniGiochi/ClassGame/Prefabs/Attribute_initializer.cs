using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attribute_initializer : MonoBehaviour
{
    public string attribute_name;

    // Inizializzi i valori dell'attributo in attribute_connection
    public void initialize()
    {
        Attribute_Connection ac = GetComponentInChildren<Attribute_Connection>();
        ac.attribute_name = attribute_name;
        ac.is_public = false;

    }
}
