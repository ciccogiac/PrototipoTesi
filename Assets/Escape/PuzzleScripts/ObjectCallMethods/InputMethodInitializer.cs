using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputMethodInitializer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputType_text;
    [SerializeField] TextMeshProUGUI inputName_text;

    private Action confirmCallback;

    public void SetInputMethodValue(string name, string value)
    {
        inputType_text.text = name;
        inputName_text.text = value;
    }

    public void EndInput()
    {
        confirmCallback?.Invoke();
    }

    public void EnableInput(Action confirmCallback)
    {
        gameObject.GetComponentInChildren<TMP_InputField>().Select();
        this.confirmCallback = confirmCallback;
    }

    public (string,string,string) GetinputValue() { return (inputName_text.text, gameObject.GetComponentInChildren<TMP_InputField>().text, inputType_text.text); }
}
