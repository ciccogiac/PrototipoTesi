using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassGame_Interface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI classname_text;

    [SerializeField] Button button_creaClasse;
    [SerializeField] Button button_eliminaClasse;

    [SerializeField] GameObject panel_noClassComplete;
    [SerializeField] GameObject panel_classDeleted;

    [SerializeField] float secondsToShowError = 3f;

    [SerializeField] InventoryLoad inventoryLoad;


    private void OnEnable()
    {
        panel_noClassComplete.SetActive(false);
        classname_text.text = DatiPersistenti.istanza.className;

        if (Inventario.istanza.classi.Contains(classname_text.text))
        {
            button_creaClasse.interactable = false;
            button_eliminaClasse.interactable = true;
        }
        else
        {
            button_creaClasse.interactable = true;
            button_eliminaClasse.interactable = false;
        }
        

    }


    public void StartGame()
    {

        foreach(var coppia in DatiPersistenti.istanza.coppie)
        {

            if (!Inventario.istanza.attributes.Contains(coppia.Key)) { StartCoroutine(ShowCluesError(secondsToShowError)); return; }

            foreach(var method in coppia.Value.Item2)
            {
                if (!Inventario.istanza.methods.Contains(method.methodName)) { StartCoroutine(ShowCluesError(secondsToShowError)); return; }
            }
        }

        SceneManager.LoadScene("ClassGame");
    }

    IEnumerator ShowCluesError(float time)
    {
        panel_noClassComplete.SetActive(true);
        yield return new WaitForSeconds(time);
        panel_noClassComplete.SetActive(false);

    }

    public void DeleteClass()
    {
        Inventario.istanza.classi.Remove(classname_text.text);

        foreach (var coppia in DatiPersistenti.istanza.coppie)
        {

            Inventario.istanza.attributes.Add(coppia.Key); 

            foreach (var method in coppia.Value.Item2)
            {
                if (!Inventario.istanza.methods.Contains(method.methodName)) { Inventario.istanza.methods.Add(method.methodName); }
            }
        }

        StartCoroutine(ShowDeleteClass(secondsToShowError));
    }

    IEnumerator ShowDeleteClass(float time)
    {
        panel_classDeleted.SetActive(true);
        button_creaClasse.interactable = true;
        button_eliminaClasse.interactable = false;
        inventoryLoad.LoadInventory();

        yield return new WaitForSeconds(time);

        panel_classDeleted.SetActive(false);

    }

    public void CloseInterface()
    {

        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
