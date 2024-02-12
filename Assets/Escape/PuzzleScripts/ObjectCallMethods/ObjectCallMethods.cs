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



    private void OnEnable()
    {
        input.enabled = false;
        interactCanvas.SetActive(false);
        objectInteraction.isActive = false;

        CallerCanvas.SetActive(true);
        SetterCanvas.SetActive(false);

        buttonCallMethod.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        objectName_text.text = objectInteraction.oggetto.oggettoEscapeValue.objectName;

        foreach (var attributo in objectInteraction.oggetto.oggettoEscapeValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attributePrefab, transform.position, Quaternion.identity);
            //setta i testi dell'attributo
            oggettoIstanziato.GetComponent<AttributeInitializer>().SetAttributeValue(attributo.attributeName,attributo.attributeValue);
            oggettoIstanziato.transform.SetParent(attributeBox.transform);
        }

        foreach (var metodo in objectInteraction.oggetto.oggettoEscapeValue.methods)
        {
            GameObject oggettoIstanziato = Instantiate(methodPrefab, transform.position, Quaternion.identity);
            //setta i valori del metodo
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = metodo.method.methodName;
            oggettoIstanziato.GetComponent<Button>().onClick.AddListener(() => SelectMethod(oggettoIstanziato));
            oggettoIstanziato.transform.SetParent(methodBox.transform);
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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        input.enabled = true;
        objectInteraction.isActive = true;

        CallerCanvas.SetActive(false);
        interactCanvas.SetActive(true);
        gameObject.SetActive(false);


    }
}
