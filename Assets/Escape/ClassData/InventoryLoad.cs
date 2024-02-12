using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryLoad : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] Inventory_Interface inventoryInterface;

    [SerializeField] TextMeshProUGUI textItems;
    [SerializeField] GameObject ItemsVisualizerVerticalBox;
    [Header("HorizontalBox")]
    [SerializeField] GameObject Box_Teoria;
    [SerializeField] GameObject Box_Methods;
    [SerializeField] GameObject Box_Attributes;
    [SerializeField] GameObject Box_Classi;
    [SerializeField] GameObject Box_Oggetti;
    [SerializeField] GameObject Box_progettoClasse;


    [Header("Prefabs")]
    [SerializeField] GameObject method_Prefab;
    [SerializeField] GameObject attribute_Prefab;
    [SerializeField] GameObject class_Prefab;
    [SerializeField] GameObject oggetto_Prefab;
    [SerializeField] GameObject teoria_Prefab;
    [SerializeField] GameObject progettoClasse_Prefab;

    [Header("ClassPanel2")]   
    [SerializeField] GameObject ClassPanel2;
    [SerializeField] TextMeshProUGUI classValueName;
    [SerializeField] GameObject ClassBox;
    [SerializeField] GameObject classValue_prefab;

    [Header("DescriptionPanel")]
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] TextMeshProUGUI ClueDescription_text;

    [Header("ObjectPanel")]
    [SerializeField] GameObject ObjectPanel;
    [SerializeField] TextMeshProUGUI ObjectClass_text;
    [SerializeField] TextMeshProUGUI ObjectDescription_text;
    [SerializeField] GameObject attributePrefab;
    [SerializeField] GameObject methodPrefab;
    [SerializeField] GameObject attributeBox;
    [SerializeField] GameObject methodBox;

    [Header("ClassPanel")]
    [SerializeField] GameObject ClassPanel;
    [SerializeField] GameObject ClassMetodiBox;
    [SerializeField] GameObject classMetodi_prefab;
    [SerializeField] GameObject ClassAttributiBox;
    [SerializeField] GameObject classAttributi_prefab;

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
        foreach (Transform figlio in Box_progettoClasse.transform) { Destroy(figlio.gameObject); }
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

        foreach (var x in Inventario.istanza.ProgettiClasse)
        {
            GameObject oggettoIstanziato = Instantiate(progettoClasse_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x.Item1;
            oggettoIstanziato.GetComponent<DescriptionInventoryButton>().InitializeDescriptionInventory(Clue.ClueType.Attributo, x.Item1, x.Item2, this);
            oggettoIstanziato.transform.SetParent(Box_progettoClasse.transform);

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
            oggettoIstanziato.GetComponent<ObjectInventoryButton>().InitializeObjectInventory(x, this);
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

            case Clue.ClueType.ProgettoClasse:
                Prefab = progettoClasse_Prefab;
                Box = Box_progettoClasse;
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

    public void AddObject(OggettoEscapeValue oggetto)
    {
        GameObject oggettoIstanziato = Instantiate(oggetto_Prefab, transform.position, Quaternion.identity);
        oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = oggetto.objectName;
        oggettoIstanziato.transform.SetParent(Box_Oggetti.transform);

        oggettoIstanziato.GetComponent<ObjectInventoryButton>().InitializeObjectInventory(oggetto, this);
        
    }

    public void ActivateItemsPanel(string s)
    {
        textItems.text = s;

       

        Box_Teoria.SetActive(false);
        Box_Methods.SetActive(false);
        Box_Attributes.SetActive(false);
        Box_Classi.SetActive(false);
        Box_Oggetti.SetActive(false);
        Box_progettoClasse.SetActive(false);
        DescriptionPanel.SetActive(false);
        ObjectPanel.SetActive(false);
        ClassPanel.SetActive(false);

        ItemsVisualizerVerticalBox.SetActive(true);

        foreach(var b in inventoryInterface.buttons)
        {
            b.interactable = true;
        }

        switch (s)
        {
            case "Teoria":
                Box_Teoria.SetActive(true);
                break;
            case "Attributi":
                Box_Attributes.SetActive(true);
                break;
            case "Metodi":
                Box_Methods.SetActive(true);
                break;
            case "ProgettoClasse":
                Box_progettoClasse.SetActive(true);
                break;
            case "Classi":
                Box_Classi.SetActive(true);
                break;
            case "Oggetti":
                Box_Oggetti.SetActive(true);
                break;
        }

    }
    

    public void ActivateDescriptionPanel(string type, string name , string description)
    {
        //InventoryPanel.SetActive(false);
        DescriptionPanel.SetActive(true);

        //ClueType_text.text = type ;
        //ClueName_text.text = name ;
        ClueDescription_text.text = description ;
    }

    public void ActivateClassPanel(ClassValue classValue)
    {
        //InventoryPanel.SetActive(false);
        ClassPanel.SetActive(true);

        //classValueName.text = classValue.className;
        foreach (Transform figlio in ClassAttributiBox.transform) { Destroy(figlio.gameObject); }
        foreach (Transform figlio in ClassMetodiBox.transform) { Destroy(figlio.gameObject); }

        Dictionary<string, List<Transform>> d = new Dictionary<string, List<Transform>>();

        foreach (var attribute in classValue.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(classAttributi_prefab, transform.position, Quaternion.identity);
            //oggettoIstanziato.GetComponentInChildren<ClassValueInventory>().SetClassValue(attribute.attribute, attribute.visibility, attribute.methods);
            oggettoIstanziato.GetComponentInChildren<AttributeInventoryInitializer>().SetAttributeValue(attribute.attribute, attribute.visibility== true ? "Public" : "Private");
            oggettoIstanziato.transform.SetParent(ClassAttributiBox.transform);

           

                foreach (var x in attribute.methods)
                {
                    if (d.ContainsKey(x.methodName))
                    {
                            d[x.methodName].Add(oggettoIstanziato.GetComponent<AttributeConnectionClassPanel>().pointLineEndAttribute);
                    }
                    else
                        {
                        List<Transform> t = new List<Transform>();
                        t.Add(oggettoIstanziato.GetComponent<AttributeConnectionClassPanel>().pointLineEndAttribute);
                        d.Add(x.methodName, t);
                        }
                }
            
        }

        foreach (var method in d)
        {
            GameObject oggettoIstanziato = Instantiate(classMetodi_prefab, transform.position, Quaternion.identity);
            //oggettoIstanziato.GetComponentInChildren<ClassValueInventory>().SetClassValue(attribute.attribute, attribute.visibility, attribute.methods);
            oggettoIstanziato.GetComponentInChildren<MethodInventoryInitializer>().SetAttributeValue(method.Key);
            
            oggettoIstanziato.transform.SetParent(ClassMetodiBox.transform);
            if(method.Value!=null)
                oggettoIstanziato.GetComponent<LineControllerClassPanel>().SetPointLine( method.Value);
        }


    }

    public void DeactivateClassPanel()
    {
        //InventoryPanel.SetActive(true);
        ClassPanel.SetActive(false);

        //classValueName.text = "";
        foreach (Transform figlio in ClassAttributiBox.transform) { Destroy(figlio.gameObject); }
    }

    public void ActivateObjectPanel(OggettoEscapeValue oggetto)
    {
        //InventoryPanel.SetActive(false);
        ObjectPanel.SetActive(true);

        //ClueType_text.text = type ;
        //ClueName_text.text = name ;
        ObjectDescription_text.text = oggetto.description;
        ObjectClass_text.text = oggetto.className;

        foreach (Transform figlio in attributeBox.transform) { Destroy(figlio.gameObject); }
        foreach (Transform figlio in methodBox.transform) { Destroy(figlio.gameObject); }

        foreach (var attributo in oggetto.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attributePrefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponent<AttributeInventoryInitializer>().SetAttributeValue(attributo.attributeName, attributo.attributeValue);
            oggettoIstanziato.transform.SetParent(attributeBox.transform);
        }

        foreach (var metodo in oggetto.methods)
        {
            GameObject oggettoIstanziato = Instantiate(methodPrefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<MethodInventoryInitializer>().SetAttributeValue(metodo.method.methodName);
            oggettoIstanziato.transform.SetParent(methodBox.transform);
        }
        

    }

}
