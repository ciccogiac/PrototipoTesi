using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ObjectGame_Interface : MonoBehaviour
{
    [SerializeField] GameObject classPrefab;
    [SerializeField] GameObject Box_Classes;
    [SerializeField] GameObject ClassBoxVertical;
    [SerializeField] GameObject noClassAvailable_text;
    [SerializeField] GameObject ObjectNameUsed_Panel;

    [SerializeField] GameObject player;
    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject Box_nameObject;
    [SerializeField] Button button_StartGame;

    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private GameObject previousButtonClass;

    [SerializeField] float secondsToShowError = 4f;

    private void OnEnable()
    {
        ObjectNameUsed_Panel.SetActive(false);

        if (Inventario.istanza.classi.Count == 0) { noClassAvailable_text.SetActive(true); ClassBoxVertical.SetActive(false); }
        else
        {
            
            noClassAvailable_text.SetActive(false);
            ClassBoxVertical.SetActive(true);
            inputField.text = "";
            Box_nameObject.SetActive(false);

            foreach (var classe in Inventario.istanza.classi)
            {
                GameObject oggettoIstanziato = Instantiate(classPrefab, transform.position, Quaternion.identity);
                oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = classe.className;
                oggettoIstanziato.GetComponent<Button>().onClick.AddListener(() => ActiveNameObjectBox(classe.className, oggettoIstanziato));
                oggettoIstanziato.transform.SetParent(Box_Classes.transform);
            }
            
        }
      
    }

    public void ActiveNameObjectBox(string className,GameObject button)
    {
        
        inputField.text = "";
        Box_nameObject.SetActive(true);
        button_StartGame.onClick.AddListener(() => StartGame(className));

        if (previousButtonClass != null) { previousButtonClass.GetComponent<Image>().color = normalColor; }
        previousButtonClass = button;
        button.GetComponent<Image>().color = selectedColor;
    }

    private bool IsNameObjectUnique()
    {
        //Controllo su oggetti raccolti nell'inventario
        foreach(var oggetto in Inventario.istanza.oggetti)
        {
            
            if (string.Equals(inputField.text, oggetto.objectName, StringComparison.OrdinalIgnoreCase)) 
            {
                StartCoroutine(ShowCluesError(secondsToShowError)) ;
                return false;
            }
        }

        //Controllo su oggetti inseriti negli objectInteractor
        foreach (var oggetto in Inventario.istanza.oggettiUsed)
        {

            if (string.Equals(inputField.text, oggetto.objectName, StringComparison.OrdinalIgnoreCase))
            {
                StartCoroutine(ShowCluesError(secondsToShowError));
                return false;
            }
        }

        //Non c'� controllo sugli oggetti preenti nell scena e non ancora raccolti. Quindi posso creare oggetto dello stesso nome , e quando
        // raccolgo l'oggetto avr� lo stesso nome di quello creato.
        //SOluzione pu� essere che se raccolgo oggetto e ne ho gi� uno con lo stesso nome , aggiungo un numero

        return true;
    }

    IEnumerator ShowCluesError(float time)
    {
        ClassBoxVertical.SetActive(false);
        ObjectNameUsed_Panel.SetActive(true);
        yield return new WaitForSeconds(time);
        ClassBoxVertical.SetActive(true);
        ObjectNameUsed_Panel.SetActive(false);

    }

    public void StartGame(string className) {
        if (inputField.text!= "" && IsNameObjectUnique()) {

            DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;
            DatiPersistenti.istanza.className = className;
            DatiPersistenti.istanza.objectName = inputField.text;
            //DatiPersistenti.istanza.coppie = FindObjectOfType<ClassDictionary>().FindClass(className);
            SceneManager.LoadScene("ObjectGame");
        }
    }

    public void CloseInterface()
    {
        

        foreach (Transform figlio in Box_Classes.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}