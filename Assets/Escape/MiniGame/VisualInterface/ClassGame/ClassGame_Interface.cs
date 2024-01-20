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
    [SerializeField] Button button_modificaClasse;

    [SerializeField] GameObject button_exit;
    [SerializeField] GameObject button_back;
    [SerializeField] GameObject button_confirm;

    [SerializeField] GameObject panel_noClassComplete;
    [SerializeField] GameObject panel_classDeleted;

    [SerializeField] GameObject panel_classOptions;
    [SerializeField] GameObject panel_classModify;

    [SerializeField] float secondsToShowError = 3f;

    [SerializeField] InventoryLoad inventoryLoad;

    private ClassValue classValue;

    [SerializeField] GameObject prefab_AttributeChangeVisibility;


    private void OnEnable()
    {
        panel_noClassComplete.SetActive(false);
        panel_classDeleted.SetActive(false);
        panel_classModify.SetActive(false);
        panel_classOptions.SetActive(true);

        button_exit.SetActive(true);
        button_back.SetActive(false);
        button_confirm.SetActive(false);

        classname_text.text = DatiPersistenti.istanza.className;

        //if (Inventario.istanza.classi.Contains(classname_text.text))
        classValue = Inventario.istanza.classi.Find(x => x.className == classname_text.text);
        if ( classValue != null)
        {
            button_creaClasse.interactable = false;
            button_eliminaClasse.interactable = true;
            button_modificaClasse.interactable = true;
        }
        else
        {
            button_creaClasse.interactable = true;
            button_eliminaClasse.interactable = false;
            button_modificaClasse.interactable = false;
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

    public void BackToClassOptions()
    {
        foreach (Transform figlio in panel_classModify.gameObject.transform) { Destroy(figlio.gameObject); }

        panel_classOptions.SetActive(true);
        button_exit.SetActive(true);
        panel_classModify.SetActive(false);
        button_back.SetActive(false);
        button_confirm.SetActive(false);
    }

    public void ConfirmClassModify()
    {

        foreach (var attributeChanged in panel_classModify.gameObject.GetComponentsInChildren<AttributeChangeVisibility>())
        {
            AttributeValue a= classValue.attributes.Find(x => x.attribute == attributeChanged.attributeName.text);
            if(a!=null) {
                //classValue.attributes.
                a.visibility= attributeChanged.toggle.isOn;
            }

        }

            foreach (Transform figlio in panel_classModify.gameObject.transform) { Destroy(figlio.gameObject); }
        CloseInterface();
    }

    public void ModifyClassShow()
    {
        panel_classOptions.SetActive(false);
        button_exit.SetActive(false);
        panel_classModify.SetActive(true);
        button_back.SetActive(true);
        button_confirm.SetActive(true);

        foreach (var a in classValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(prefab_AttributeChangeVisibility, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<AttributeChangeVisibility>().InitializeAttribute(a.attribute,a.visibility);
            oggettoIstanziato.transform.SetParent(panel_classModify.transform);

            
        }
        
    }

    public void DeleteClass()
    {
        //Inventario.istanza.classi.Remove(classname_text.text);
        ClassValue c = Inventario.istanza.classi.Find(x => x.className == classname_text.text);
        if(c!= null) { 

        foreach (var coppia in c.attributes)
        {

            Inventario.istanza.attributes.Add(coppia.attribute); 

            foreach (var method in coppia.methods)
            {
                if (!Inventario.istanza.methods.Contains(method.methodName)) { Inventario.istanza.methods.Add(method.methodName); }
            }
        }

            Inventario.istanza.classi.Remove(c);
        }

        OggettoEscape[] oggettiTrovati = Object.FindObjectsOfType<OggettoEscape>();

        // Stampa i risultati
        foreach (OggettoEscape oggetto in oggettiTrovati)
        {
            if(oggetto.oggettoEscapeValue.className == classname_text.text)
                Destroy(oggetto.gameObject);
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
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

   
}
