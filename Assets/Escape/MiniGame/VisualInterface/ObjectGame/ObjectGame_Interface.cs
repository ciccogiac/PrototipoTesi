using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class ObjectGame_Interface : MonoBehaviour
{
    [SerializeField] GameObject classPrefab;
    [SerializeField] GameObject Box_Classes;
    [SerializeField] GameObject ClassBoxVertical;
    [SerializeField] GameObject noClassAvailable_text;
    [SerializeField] GameObject ObjectNameUsed_Panel;
    [SerializeField] GameObject loadingBox;

    [SerializeField] GameObject player;
    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject Box_nameObject;
    [SerializeField] Button button_StartGame;

    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private GameObject previousButtonClass;

    [SerializeField] float secondsToShowError = 4f;

    [SerializeField] GameObject interactCanvas;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] PlayerInput input;

    [SerializeField] LevelHint _levelHint;

    private void OnEnable()
    {
        input.enabled = false;

        interactCanvas.SetActive(false);
        ObjectNameUsed_Panel.SetActive(false);
        loadingBox.SetActive(false);

        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);


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

        //Non c'è controllo sugli oggetti preenti nell scena e non ancora raccolti. Quindi posso creare oggetto dello stesso nome , e quando
        // raccolgo l'oggetto avrà lo stesso nome di quello creato.
        //SOluzione può essere che se raccolgo oggetto e ne ho già uno con lo stesso nome , aggiungo un numero

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

            ClassBoxVertical.SetActive(false);
            loadingBox.SetActive(true);

            DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;
            DatiPersistenti.istanza.lastCharacterEscapeRotation = player.transform.rotation;
            DatiPersistenti.istanza.className = className;
            DatiPersistenti.istanza.objectName = inputField.text;
            DatiPersistenti.istanza.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            DatiPersistenti.istanza.hintCounter = _levelHint.hintCounter;
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
        interactCanvas.SetActive(true);

        input.enabled = true;
    }
}
