using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttributeInventoryInitializer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attributeName_text;
    [SerializeField] TextMeshProUGUI attributeValue_text;

    public void SetAttributeValue(string name, string value)
    {
        attributeName_text.text = name;
        attributeValue_text.text = value;
    }
}
