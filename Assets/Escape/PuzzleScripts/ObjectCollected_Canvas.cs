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

    [SerializeField] PlayerInput input;

    public ObjectInteraction objectInteraction;

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
                oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = oggetto;
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
        button.GetComponent<Image>().color = selectedColor;
    }

    private void SelectObjectOnGame()
    {
        GameObject oggettoIstanziato = Instantiate(objectPrefab,objectInteraction.objectPoint.position, Quaternion.identity);
        oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

        objectInteraction.oggetto = oggettoIstanziato.GetComponent<Clue>();
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
        input.enabled = true;
        objectInteraction.isActive = true;

        gameObject.SetActive(false);
      
    }
}
