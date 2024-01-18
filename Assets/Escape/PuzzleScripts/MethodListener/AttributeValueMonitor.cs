using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AttributeValueMonitor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_AttributeName;
    [SerializeField] TextMeshProUGUI text_AttributeValue;

    public void SetAttributeValueMonitor(string attributeName,string attributeValue)
    {
        text_AttributeName.text = attributeName;
        text_AttributeValue.text = attributeValue;
    }
}
