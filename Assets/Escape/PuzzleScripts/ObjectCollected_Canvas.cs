using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectCollected_Canvas : MonoBehaviour
{
    [SerializeField] GameObject objectCollectedCanvasPrefab;
    [SerializeField] GameObject objectPrefab;

    [SerializeField] GameObject ObjectBox;
    [SerializeField] GameObject noObjectAvailable_text;
    [SerializeField] GameObject buttonSelectObject;

    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private GameObject previousObjectButton;
    private string objectName;

    [SerializeField] PlayerInput input;
    [SerializeField] GameObject interactCanvas;


    public ObjectInteraction objectInteraction;

    [SerializeField] InventoryLoad inventoryLoad;

    private void OnEnable()
    {
        input.enabled = false;
        interactCanvas.SetActive(false);
        objectInteraction.isActive = false;

        buttonSelectObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        buttonSelectObject.GetComponent<Button>().onClick.AddListener(() => SelectObjectOnGame());

        if (Inventario.istanza.oggetti.Count == 0) { noObjectAvailable_text.SetActive(true); ObjectBox.SetActive(false); }
        else
        {

            noObjectAvailable_text.SetActive(false);
            ObjectBox.SetActive(true);

            foreach (var oggetto in Inventario.istanza.oggetti)
            {
                GameObject oggettoIstanziato = Instantiate(objectCollectedCanvasPrefab, transform.position, Quaternion.identity);
                oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = oggetto.objectName;
                oggettoIstanziato.GetComponent<Button>().onClick.AddListener(() => SelectObjectOnCanvas(oggettoIstanziato));
                oggettoIstanziato.transform.SetParent(ObjectBox.transform);
            }

        }
    }

    private void SelectObjectOnCanvas(GameObject button)
    {
        buttonSelectObject.SetActive(true);

        if (previousObjectButton != null) { previousObjectButton.GetComponent<Image>().color = normalColor; }
        previousObjectButton = button;
        objectName = button.GetComponentInChildren<TextMeshProUGUI>().text;
        button.GetComponent<Image>().color = selectedColor;
    }

    private void SelectObjectOnGame()
    {
        if (objectInteraction.oggetto == null)
        {
            OggettoEscapeValue oggettoEscapeValue = Inventario.istanza.oggetti.Find(x => x.objectName == objectName);
            oggettoEscapeValue.ObjectInteractorId = objectInteraction.Id;
            GameObject oggettoIstanziato = Instantiate(objectPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
            oggettoIstanziato.GetComponent<OggettoEscape>().SetOggettoEscapeValue( oggettoEscapeValue);

            oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
            oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

            oggettoIstanziato.gameObject.GetComponent<MeshFilter>().mesh = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.mesh;
            oggettoIstanziato.gameObject.GetComponent<MeshRenderer>().materials = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.material;
            float fattoreScala = 0.5f;
            oggettoIstanziato.gameObject.transform.localScale *= fattoreScala;
            oggettoIstanziato.gameObject.SetActive(true);
            
            //oggettoIstanziato.GetComponent<OggettoEscape>().SetObjectValue();
            objectInteraction.oggetto = oggettoIstanziato.GetComponent<OggettoEscape>();
            objectInteraction.oggetto.methodListener = objectInteraction.methodListener;
            objectInteraction.oggetto.methodListener.SetClass( objectInteraction.oggetto.oggettoEscapeValue.className);

            Inventario.istanza.oggetti.Remove(oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue);
            inventoryLoad.RemoveObject(objectName);
            Inventario.istanza.oggettiUsed.Add(oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue);

        }
        CloseInterface();
    }

    public void CloseInterface()
    {


        foreach (Transform figlio in ObjectBox.gameObject.transform)
        {
            // Elimina il figlio corrente
            Destroy(figlio.gameObject);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        input.enabled = true;
        objectInteraction.isActive = true;
        interactCanvas.SetActive(true);

        gameObject.SetActive(false);
      
    }
}
