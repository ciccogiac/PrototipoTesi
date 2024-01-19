using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ObjectGame_Interface : MonoBehaviour
{
    [SerializeField] GameObject classPrefab;
    [SerializeField] GameObject Box_Classes;
    [SerializeField] GameObject ClassBoxVertical;
    [SerializeField] GameObject noClassAvailable_text;

    [SerializeField] GameObject player;
    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject Box_nameObject;
    [SerializeField] Button button_StartGame;

    [SerializeField] Color selectedColor;
    [SerializeField] Color normalColor;
    private GameObject previousButtonClass; 

    private void OnEnable()
    {
        if(Inventario.istanza.classi.Count == 0) { noClassAvailable_text.SetActive(true); ClassBoxVertical.SetActive(false); }
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

    public void StartGame(string className) {
        if (inputField.text!= "") {
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
