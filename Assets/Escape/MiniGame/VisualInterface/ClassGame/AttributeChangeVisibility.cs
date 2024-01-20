using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttributeChangeVisibility : MonoBehaviour
{
    public TextMeshProUGUI attributeName;
    public TextMeshProUGUI attributeVisibility;
    public Toggle toggle;

    public void InitializeAttribute(string name,bool visible)
    {
        attributeName.text = name;

        if (visible)
        {
            attributeVisibility.text = "Public";
            toggle.isOn = true;
        }
        else
        {
            attributeVisibility.text = "Private";
            toggle.isOn = false;
        }
    }

    public void ChangeVisibilityText()
    {
        if (toggle.isOn)
        {
            attributeVisibility.text = "Public";
        }
        else
        {
            attributeVisibility.text = "Private";
        }
    }
}
