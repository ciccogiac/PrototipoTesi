using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassGame_Interface : MonoBehaviour
{
    [SerializeField] GameObject classStarterPrefab;

    [SerializeField] TextMeshProUGUI classname_text;

    [SerializeField] Button button_creaClasse;
    [SerializeField] Button button_eliminaClasse;
    [SerializeField] Button button_modificaClasse;

    [SerializeField] GameObject button_exit;
    [SerializeField] GameObject button_confirm;

    [SerializeField] GameObject panel_noClassComplete;
    [SerializeField] GameObject panel_classDeleted;

    [SerializeField] GameObject panel_classOptions;
    [SerializeField] GameObject panel_classModify;

    [SerializeField] GameObject panel_classBox;
    [SerializeField] GameObject panel_classStarter;
    [SerializeField] GameObject BoxClassStarter;
    [SerializeField] GameObject panel_noClassStarter;

    [SerializeField] GameObject backButton;

    [SerializeField] float secondsToShowError = 3f;

    [SerializeField] InventoryLoad inventoryLoad;

    private ClassValue classValue;

    [SerializeField] GameObject prefab_AttributeChangeVisibility;

    [SerializeField] GameObject interactCanvas;
    [SerializeField] PlayerInput input;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private bool modifyOpen = false;

    [SerializeField] LevelHint _levelHint;

    [SerializeField] bool isClassModify = true;

    private void OnEnable()
    {
        input.enabled = false;
        interactCanvas.SetActive(false);      
        panel_classBox.SetActive(false);
        backButton.SetActive(false);

        panel_noClassComplete.SetActive(false);
        panel_classDeleted.SetActive(false);
        panel_classModify.SetActive(false);
        panel_classOptions.SetActive(true);

       

        button_exit.SetActive(true);
        button_confirm.SetActive(false);

        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);



        if (Inventario.istanza.ProgettiClasse.Count == 0) { panel_noClassStarter.SetActive(true); panel_classStarter.SetActive(false); }
        else
        {

            panel_noClassStarter.SetActive(false);
            panel_classStarter.SetActive(true);

            foreach (var progettoClasse in Inventario.istanza.ProgettiClasse)
            {
                GameObject oggettoIstanziato = Instantiate(classStarterPrefab, transform.position, Quaternion.identity);
                oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = progettoClasse.Item1;
                oggettoIstanziato.GetComponent<Button>().onClick.AddListener(() => OpenClass(progettoClasse.Item1));
                oggettoIstanziato.transform.SetParent(BoxClassStarter.transform);
            }

        }




    }

    public void CloseOpenClassPanel()
    {
        if (modifyOpen)
        { 
            modifyOpen = false;
            panel_classOptions.SetActive(true);
            panel_classModify.SetActive(false);
            foreach (Transform figlio in panel_classModify.gameObject.transform)
                    { Destroy(figlio.gameObject); }
            button_confirm.SetActive(false);
            panel_classBox.SetActive(true); 
            return; 
        }

        backButton.SetActive(false);
        panel_classBox.SetActive(false);
        panel_classStarter.SetActive(true);

        
        
    }

    private void OpenClass(string className)
    {
        modifyOpen = false;


        panel_classStarter.SetActive(false);
        panel_classModify.SetActive(false);
        panel_classBox.SetActive(true);
        backButton.SetActive(true);

        DatiPersistenti.istanza.className = className;
        DatiPersistenti.istanza.coppie = FindObjectOfType<ClassDictionary>().FindClass(className); ;

        classname_text.text = className;

        classValue = Inventario.istanza.classi.Find(x => x.className == className);
        if (classValue != null)
        {
            /*
            button_creaClasse.interactable = false;
            button_eliminaClasse.interactable = true;
            button_modificaClasse.interactable = true;
            */

            button_creaClasse.gameObject.SetActive(false);
            button_eliminaClasse.gameObject.SetActive(true);

            if(isClassModify)
                button_modificaClasse.gameObject.SetActive(true);
            else
                button_modificaClasse.gameObject.SetActive(false);
        }
        else
        {   /*
            button_creaClasse.interactable = true;
            button_eliminaClasse.interactable = false;
            button_modificaClasse.interactable = false;
            */

            button_creaClasse.gameObject.SetActive(true);
            button_eliminaClasse.gameObject.SetActive(false);
            button_modificaClasse.gameObject.SetActive(false);
        }
    }


    public void StartGame()
    {
        if (DatiPersistenti.istanza.coppie == null) return;

        foreach(var coppia in DatiPersistenti.istanza.coppie)
        {
            (string,string) a =Inventario.istanza.attributes.Find(x => x.Item1 == coppia.Key);
            if (a == (null,null)) { StartCoroutine(ShowCluesError(secondsToShowError)); return; }

            foreach(var method in coppia.Value.Item2)
            {
                (string, string) b = Inventario.istanza.methods.Find(x => x.Item1 == method.methodName);
                if (b == (null,null)) { StartCoroutine(ShowCluesError(secondsToShowError)); return; }
            }
        }
        DatiPersistenti.istanza.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        DatiPersistenti.istanza.hintCounter = _levelHint.hintCounter;

        SceneManager.LoadScene("ClassGame");
    }

    IEnumerator ShowCluesError(float time)
    {
        backButton.SetActive(false);
        panel_classBox.SetActive(false);
        panel_noClassComplete.SetActive(true);
        yield return new WaitForSeconds(time);
        panel_noClassComplete.SetActive(false);
        panel_classBox.SetActive(true);
        backButton.SetActive(true);

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

        modifyOpen = false;
        CloseInterface();
    }

    public void ModifyClassShow()
    {
        modifyOpen = true;

        panel_classOptions.SetActive(false);
        panel_classModify.SetActive(true);
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
            (string,string) tupla = Inventario.istanza.methodsAttributesUsed.Find(x => x.Item1 == coppia.attribute);
                //Inventario.istanza.attributes.Add((coppia.attribute,"")); 
                if (tupla != (null, null))
                {
                    Inventario.istanza.attributes.Add(tupla);
                    Inventario.istanza.methodsAttributesUsed.Remove(tupla);
                }

                foreach (var method in coppia.methods)
            {
                    
                    if (Inventario.istanza.methods.Find(x => x.Item1 == method.methodName) == (null,null)) {
                        (string, string) tupla_m = Inventario.istanza.methodsAttributesUsed.Find(x => x.Item1 == method.methodName);
                        if (tupla_m != (null, null))
                        {
                            Inventario.istanza.methods.Add(tupla_m);
                            Inventario.istanza.methodsAttributesUsed.Remove(tupla_m);
                        }
                    }
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
        backButton.SetActive(false);
        panel_classBox.SetActive(false);
        panel_classDeleted.SetActive(true);

        /*
        button_creaClasse.interactable = true;
        button_eliminaClasse.interactable = false;
        button_modificaClasse.interactable = false;
        */

        button_creaClasse.gameObject.SetActive(true);
        button_eliminaClasse.gameObject.SetActive(false);
        button_modificaClasse.gameObject.SetActive(false);

        inventoryLoad.LoadInventory();

        yield return new WaitForSeconds(time);

        panel_classDeleted.SetActive(false);
        panel_classBox.SetActive(true);
        backButton.SetActive(true);
    }

    public void CloseInterface()
    {
        foreach (Transform figlio in BoxClassStarter.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        if(modifyOpen)
            foreach (Transform figlio in panel_classModify.gameObject.transform)
            { Destroy(figlio.gameObject); }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);

        input.enabled = true;
        interactCanvas.SetActive(true);
    }

   
}
