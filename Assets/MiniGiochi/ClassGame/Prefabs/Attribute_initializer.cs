using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Attribute_initializer : MonoBehaviour
{
    public string attribute_name;
    public Button visibilityButton;

    // Inizializzi i valori dell'attributo in attribute_connection
    public void initialize(bool visibility , bool isPrivateNonVisibilityLevel)
    {
        Attribute_Connection ac = GetComponentInChildren<Attribute_Connection>();
        ac.attribute_name = attribute_name;

        if (isPrivateNonVisibilityLevel)
        {
            ac.is_public = false;
            visibilityButton.interactable = false;
        }

        else
        {
            ac.is_public = !visibility;
            visibilityButton.interactable = visibility;
        }
       
        
    }
}
