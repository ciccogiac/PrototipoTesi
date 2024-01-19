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

    public ObjectInteraction objectInteraction;

    [SerializeField] InventoryLoad inventoryLoad;

    private void OnEnable()
    {
        input.enabled = false;
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
            OggettoEscape oggettoIstanziato = Inventario.istanza.oggetti.Find(x => x.objectName == objectName);
            //GameObject oggettoIstanziato = Instantiate(objectPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
            oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
            oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);
            //oggettoIstanziato.gameObject.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
            oggettoIstanziato.gameObject.AddComponent<MeshFilter>().mesh = oggettoIstanziato.mesh;
            oggettoIstanziato.gameObject.AddComponent<MeshRenderer>().material = oggettoIstanziato.material;
            float fattoreScala = 0.3f;
            oggettoIstanziato.gameObject.transform.localScale *= fattoreScala;
            oggettoIstanziato.gameObject.SetActive(true);
            
            //oggettoIstanziato.GetComponent<OggettoEscape>().SetObjectValue();
            objectInteraction.oggetto = oggettoIstanziato;
            objectInteraction.oggetto.methodListener = objectInteraction.methodListener;
            objectInteraction.oggetto.methodListener.className = objectInteraction.oggetto.className;

            Inventario.istanza.oggetti.Remove(oggettoIstanziato);
            inventoryLoad.RemoveObject(objectName);

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

        gameObject.SetActive(false);
      
    }
}
