using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MethodInventoryInitializer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI methodName_text;

    public void SetAttributeValue(string name)
    {
        methodName_text.text = name;
    }
}
