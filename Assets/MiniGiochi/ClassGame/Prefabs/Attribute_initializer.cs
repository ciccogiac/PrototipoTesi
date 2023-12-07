using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attribute_initializer : MonoBehaviour
{
    public string attribute_name;

    // Start is called before the first frame update
    public void initialize()
    {
        Attribute_Connection ac = GetComponentInChildren<Attribute_Connection>();
        ac.attribute_name = attribute_name;
        ac.is_public = false;

    }
}
