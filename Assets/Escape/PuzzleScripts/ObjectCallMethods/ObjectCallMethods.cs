using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectCallMethods : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectName_text;

    [SerializeField] GameObject attributePrefab;
    [SerializeField] GameObject methodPrefab;

    [SerializeField] GameObject attributeBox;
    [SerializeField] GameObject methodBox;

    [SerializeField] GameObject buttonCallMethod;

    public PlayerInput input;
    [SerializeField] GameObject interactCanvas;
    [SerializeField] GameObject interactSwitchCameraCanvas;
    public ObjectInteraction objectInteraction;

    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private GameObject previousObjectButton;

    private string methodCaller;

    public GameObject CallerCanvas;
    public GameObject SetterCanvas;
    public SetterMethod setterMethod;

    public GameObject InputCanvas;
    public InputMethod inputMethod;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] GameManager_Escape gameManager;

    public bool isTutorialStarted = false;

    private void OnEnable()
    {
        input.enabled = false;
        if (objectInteraction.isObjectPermanent || objectInteraction.isObjectSee) { interactSwitchCameraCanvas.SetActive(false); gameManager.isSeeing = false; }
        interactCanvas.SetActive(false);
        objectInteraction.isActive = false;

        CallerCanvas.SetActive(true);
        SetterCanvas.SetActive(false);

        buttonCallMethod.SetActive(false);
        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        objectName_text.text = objectInteraction.oggetto.oggettoEscapeValue.objectName;

        foreach (var attributo in objectInteraction.oggetto.oggettoEscapeValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attributePrefab, transform.position, Quaternion.identity);
            //setta i testi dell'attributo
            oggettoIstanziato.GetComponent<AttributeInitializer>().SetAttributeValue(attributo.attributeName,attributo.attributeValue);
            oggettoIstanziato.transform.SetParent(attributeBox.transform);
            oggettoIstanziato.transform.localScale = Vector3.one;
        }

        foreach (var metodo in objectInteraction.oggetto.oggettoEscapeValue.methods)
        {
            GameObject oggettoIstanziato = Instantiate(methodPrefab, transform.position, Quaternion.identity);
            //setta i valori del metodo
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = metodo.method.methodName;
            oggettoIstanziato.GetComponent<Button>().onClick.AddListener(() => SelectMethod(oggettoIstanziato));
            oggettoIstanziato.transform.SetParent(methodBox.transform);
            oggettoIstanziato.transform.localScale = Vector3.one;
        }
        


    }

    public void ReloadCallerCanvas()
    {
        foreach(Transform a in attributeBox.transform) { Destroy(a.gameObject); }

        foreach (var attributo in objectInteraction.oggetto.oggettoEscapeValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attributePrefab, transform.position, Quaternion.identity);
            //setta i testi dell'attributo
            oggettoIstanziato.GetComponent<AttributeInitializer>().SetAttributeValue(attributo.attributeName, attributo.attributeValue);
            oggettoIstanziato.transform.SetParent(attributeBox.transform);
            oggettoIstanziato.transform.localScale = Vector3.one;
        }
    }

    private void SelectMethod(GameObject button) {
        buttonCallMethod.SetActive(true);
        methodCaller = button.GetComponentInChildren<TextMeshProUGUI>().text;

        //if (previousObjectButton != null) { previousObjectButton.GetComponent<Image>().color = normalColor; }
        previousObjectButton = button;
        //button.GetComponent<Image>().color = selectedColor;
    }

    public void CallMethod()
    {
        //Debug.Log("MethodCalled");
        objectInteraction.oggetto.ObjectCallCanvas = this;
        objectInteraction.oggetto.CallMethod(methodCaller);

    }

    public void CloseInterface()
    {


        foreach (Transform figlio in attributeBox.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        foreach (Transform figlio in methodBox.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }


        
        objectInteraction.isActive = true;
        CallerCanvas.SetActive(false);

        if (isTutorialStarted) { isTutorialStarted = false; gameObject.SetActive(false); return; }

        input.enabled = true;
        if (objectInteraction.isObjectPermanent || objectInteraction.isObjectSee) 
        {
            Cursor.SetCursor(gameManager.cursorSwitchCameraTexture, new Vector2(gameManager.cursorSwitchCameraTexture.width / 2, gameManager.cursorSwitchCameraTexture.height / 2), CursorMode.Auto);
            Cursor.visible = true; Cursor.lockState = CursorLockMode.Confined;
            objectInteraction.gameObject.GetComponent<Outline>().enabled = false;
            interactSwitchCameraCanvas.SetActive(true); gameManager.isSeeing = true;
            input.SwitchCurrentActionMap("SwitchCamera");
        }
        else{ Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; interactCanvas.SetActive(true); }

        gameObject.SetActive(false);


    }
}
