using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputMethodInitializer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputType_text;
    [SerializeField] TextMeshProUGUI inputName_text;

    public void SetInputMethodValue(string name, string value)
    {
        inputType_text.text = name;
        inputName_text.text = value;
    }

    public (string,string,string) GetinputValue() { return (inputName_text.text, gameObject.GetComponentInChildren<TMP_InputField>().text, inputType_text.text); }
}
