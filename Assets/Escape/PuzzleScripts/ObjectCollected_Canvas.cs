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

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] GameManager_Escape gameManager;

    [SerializeField] GameObject interactSwitchCameraCanvas;

    private void OnEnable()
    {
        input.enabled = false;
        if (objectInteraction.isObjectSee) { interactSwitchCameraCanvas.SetActive(false); gameManager.isSeeing = false; }
        interactCanvas.SetActive(false);
        objectInteraction.isActive = false;
        buttonSelectObject.SetActive(false);

        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
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
            //GameObject oggettoIstanziato = Instantiate(objectPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
            GameObject oggettoIstanziato = Instantiate(oggettoEscapeValue.classPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
            oggettoIstanziato.GetComponent<OggettoEscape>().SetOggettoEscapeValue( oggettoEscapeValue);
            oggettoIstanziato.GetComponent<OggettoEscape>().isActive = false;

            oggettoIstanziato.GetComponent<Collider>().enabled = false;

            oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
            oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

            //oggettoIstanziato.gameObject.GetComponent<MeshFilter>().mesh = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.mesh;
            //oggettoIstanziato.gameObject.GetComponent<MeshRenderer>().materials = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.material;
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


        input.enabled = true;
        objectInteraction.isActive = true;

        if (objectInteraction.isObjectSee)
        {
            Cursor.SetCursor(gameManager.cursorSwitchCameraTexture, new Vector2(gameManager.cursorSwitchCameraTexture.width / 2, gameManager.cursorSwitchCameraTexture.height / 2), CursorMode.Auto);
            Cursor.visible = true; Cursor.lockState = CursorLockMode.Confined;
            objectInteraction.gameObject.GetComponent<Outline>().enabled = false;
            interactSwitchCameraCanvas.SetActive(true); gameManager.isSeeing = true;
            input.SwitchCurrentActionMap("SwitchCamera");
        }
        else { Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; interactCanvas.SetActive(true); }


        gameObject.SetActive(false);
      
    }
}
