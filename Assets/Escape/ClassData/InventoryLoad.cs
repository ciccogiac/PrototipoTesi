using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryLoad : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    [Header("HorizontalBox")]
    [SerializeField] GameObject Box_Teoria;
    [SerializeField] GameObject Box_Methods;
    [SerializeField] GameObject Box_Attributes;
    [SerializeField] GameObject Box_Classi;
    [SerializeField] GameObject Box_Oggetti;

    [Header("Prefabs")]
    [SerializeField] GameObject method_Prefab;
    [SerializeField] GameObject attribute_Prefab;
    [SerializeField] GameObject class_Prefab;
    [SerializeField] GameObject oggetto_Prefab;
    [SerializeField] GameObject teoria_Prefab;

    [Header("ClassPanel")]   
    [SerializeField] GameObject ClassPanel;
    [SerializeField] TextMeshProUGUI classValueName;
    [SerializeField] GameObject ClassBox;
    [SerializeField] GameObject classValue_prefab;

    [Header("DescriptionPanel")]
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] TextMeshProUGUI ClueType_text;
    [SerializeField] TextMeshProUGUI ClueName_text;
    [SerializeField] TextMeshProUGUI ClueDescription_text;

    // Start is called before the first frame update
    void Start()
    {
        LoadInventory();
    }

    public void RemoveObject(string objectName)
    {
        foreach (Transform figlio in Box_Oggetti.transform) { if (figlio.GetComponentInChildren<TextMeshProUGUI>().text == objectName) { Destroy(figlio.gameObject); } }
    }



    public void LoadInventory()
    {
        foreach (Transform figlio in Box_Teoria.transform){Destroy(figlio.gameObject);}
        foreach (Transform figlio in Box_Methods.transform) { Destroy(figlio.gameObject); }
        foreach (Transform figlio in Box_Attributes.transform) { Destroy(figlio.gameObject); }
        foreach (Transform figlio in Box_Classi.transform) { Destroy(figlio.gameObject); }
        foreach (Transform figlio in Box_Oggetti.transform) { Destroy(figlio.gameObject); }

        foreach (var x in Inventario.istanza.methods)
        {
            GameObject oggettoIstanziato = Instantiate(method_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.Item1;
            oggettoIstanziato.GetComponent<DescriptionInventoryButton>().InitializeDescriptionInventory(Clue.ClueType.Metodo, x.Item1, x.Item2, this);
            oggettoIstanziato.transform.SetParent(Box_Methods.transform);

        }

        foreach (var x in Inventario.istanza.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attribute_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.Item1;
            oggettoIstanziato.GetComponent<DescriptionInventoryButton>().InitializeDescriptionInventory(Clue.ClueType.Attributo, x.Item1, x.Item2, this);
            oggettoIstanziato.transform.SetParent(Box_Attributes.transform);

        }

        foreach (var x in Inventario.istanza.classi)
        {
            GameObject oggettoIstanziato = Instantiate(class_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.className;
            oggettoIstanziato.GetComponent<ClassInventoryButton>().InitializeClassInventoryVisualization(x.className,this);
            oggettoIstanziato.transform.SetParent(Box_Classi.transform);

        }

        foreach (var x in Inventario.istanza.oggetti)
        {
            GameObject oggettoIstanziato = Instantiate(oggetto_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.objectName;
            oggettoIstanziato.transform.SetParent(Box_Oggetti.transform);

        }

        foreach (var x in Inventario.istanza.teoria)
        {
            GameObject oggettoIstanziato = Instantiate(teoria_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.Item1;
            oggettoIstanziato.GetComponent<DescriptionInventoryButton>().InitializeDescriptionInventory(Clue.ClueType.Teoria, x.Item1, x.Item2, this);
            oggettoIstanziato.transform.SetParent(Box_Teoria.transform);

        }
    }

    public void AddItem(string clueText,string clueDescription, Clue.ClueType clueType)
    {
        GameObject Prefab=null;
        GameObject Box=null;
        switch (clueType)
        {
            case Clue.ClueType.Teoria:
                Prefab=teoria_Prefab;
                Box = Box_Teoria;
                break;

            case Clue.ClueType.Attributo:
                Prefab = attribute_Prefab;
                Box = Box_Attributes;
                break;

            case Clue.ClueType.Metodo:
                Prefab = method_Prefab;
                Box = Box_Methods;
                break;

            case Clue.ClueType.Classe:
                Prefab = class_Prefab;
                Box = Box_Classi;
                break;

            case Clue.ClueType.Oggetto:
                Prefab = oggetto_Prefab;
                Box = Box_Oggetti;
                break;
        }

        GameObject oggettoIstanziato = Instantiate(Prefab, transform.position, Quaternion.identity);
        oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = clueText;
        oggettoIstanziato.transform.SetParent(Box.transform);

        if (clueType == Clue.ClueType.Classe)
        {
            oggettoIstanziato.GetComponent<ClassInventoryButton>().InitializeClassInventoryVisualization(clueText,this);
            return;
        }

        if (clueType != Clue.ClueType.Oggetto)
        {
            oggettoIstanziato.GetComponent<DescriptionInventoryButton>().InitializeDescriptionInventory(clueType,clueText,clueDescription,this);
        }

        }

    public void ActivateClassPanel(ClassValue classValue)
    {
        InventoryPanel.SetActive(false);
        ClassPanel.SetActive(true);

        classValueName.text = classValue.className;

        foreach(var attribute in classValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(classValue_prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<ClassValueInventory>().SetClassValue(attribute.attribute,attribute.visibility,attribute.methods);
            oggettoIstanziato.transform.SetParent(ClassBox.transform);
        }

    }

    public void ActivateDescriptionPanel(string type, string name , string description)
    {
        InventoryPanel.SetActive(false);
        DescriptionPanel.SetActive(true);

        ClueType_text.text = type ;
        ClueName_text.text = name ;
        ClueDescription_text.text = description ;
    }

    public void DeactivateClassPanel()
    {
        InventoryPanel.SetActive(true);
        ClassPanel.SetActive(false);

        classValueName.text = "";
        foreach (Transform figlio in ClassBox.transform) { Destroy(figlio.gameObject); }
    }

}
