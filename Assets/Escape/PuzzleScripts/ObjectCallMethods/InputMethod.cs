using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputMethod : MonoBehaviour
{
    [SerializeField] GameObject prefab_methodInput;
    [SerializeField] GameObject box_methodInput;
    [SerializeField] ObjectCallMethods objectCallMethods;

    public TextMeshProUGUI method_text;
    public List<(string, string)> attributeValues;

    [SerializeField] GameObject wrongInputBox;
    [SerializeField] GameObject buttonBox;
    [SerializeField] GameObject buttonBoxError;

    public TextMeshProUGUI error_text;
    public TextMeshProUGUI hint_text;

    [SerializeField] GameObject box_Caller;

    public void CreateMethodInput(string name, string value)
    {
        GameObject oggettoIstanziato = Instantiate(prefab_methodInput, transform.position, Quaternion.identity);
        oggettoIstanziato.transform.SetParent(box_methodInput.transform);
        oggettoIstanziato.transform.localScale = Vector3.one;
        oggettoIstanziato.GetComponent<InputMethodInitializer>().SetInputMethodValue(name,value);

    }


    public void ShowInputError()
    {
        box_methodInput.SetActive(false);
        buttonBox.SetActive(false);

        wrongInputBox.SetActive(true);
        buttonBoxError.SetActive(true);

        

    }

    public void HideInputError()
    {
        wrongInputBox.SetActive(false);
        buttonBoxError.SetActive(false);

        box_methodInput.SetActive(true);
        buttonBox.SetActive(true);
    
    }

    public void ConfirmInputMethod()
    {
        List<(string, string)> attributesInputValues = new List<(string, string)>();
        foreach (Transform figlio in box_methodInput.gameObject.transform)
        {
            (string, string,string) tupla = figlio.GetComponent<InputMethodInitializer>().GetinputValue();
            

            switch (tupla.Item3)
            {
                case "int":
                    if (int.TryParse(tupla.Item2, out int result))
                    {
                        Debug.Log("La stringa � un numero intero.");
                        break;
                    }
                    else
                    {
                        Debug.Log("Il valore di " + tupla.Item1 + " non � un numero intero.");
                        error_text.text = "Il valore di " + tupla.Item1 + " non � un numero intero.";
                        hint_text.text = "Prova ad inserire una numero intero , senza virgola o altri caratteri aggiuntivi";
                        ShowInputError();
                        return;
                    }
                    break;
                case "bool":
                    if (string.Equals(tupla.Item2, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(tupla.Item2, "false", StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log("La stringa � un booleano.");
                        break;
                    }
                    else
                    {
                        Debug.Log("Il valore di " + tupla.Item1 + " non � un booleano.");
                        error_text.text = "Il valore di " + tupla.Item1 + " non � un booleano.";
                        hint_text.text = "Prova ad inserire un valore true o false , senza altri caratteri aggiuntivi";
                        ShowInputError();
                        return;
                    }
                    break;
                case "Char":
                    if (tupla.Item2.Length==1)
                    {
                        Debug.Log("La stringa � un char.");
                        break;
                    }
                    else
                    {
                        Debug.Log("Il valore di " + tupla.Item1 + " non � un char.");
                        error_text.text = "Il valore di " + tupla.Item1 + " non e' un char.";
                        hint_text.text = "Prova ad inserire un carattere alfanumerico , senza altri caratteri aggiuntivi";
                        ShowInputError();
                        return;
                    }
                    break;
                default:
                    break;
            }

            attributesInputValues.Add((tupla.Item1, tupla.Item2));

        }

        objectCallMethods.objectInteraction.methodListener.MethodInput(attributeValues,attributesInputValues);
       // CloseInterface();


        foreach (Transform figlio in box_methodInput.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        gameObject.SetActive(false);
        objectCallMethods.CloseInterface();
    }

    public void CloseInterface()
    {
        foreach (Transform figlio in box_methodInput.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        gameObject.SetActive(false);
        box_Caller.SetActive(true);
        //objectCallMethods.CloseInterface();
    }
}
