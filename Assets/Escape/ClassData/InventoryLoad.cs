using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryLoad : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        LoadInventory();
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
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x;
            oggettoIstanziato.transform.SetParent(Box_Methods.transform);

        }

        foreach (var x in Inventario.istanza.attributes)
        {
            GameObject oggettoIstanziato = Instantiate(attribute_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x;
            oggettoIstanziato.transform.SetParent(Box_Attributes.transform);

        }

        foreach (var x in Inventario.istanza.classi)
        {
            GameObject oggettoIstanziato = Instantiate(class_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x;
            oggettoIstanziato.transform.SetParent(Box_Classi.transform);

        }

        foreach (var x in Inventario.istanza.oggetti)
        {
            GameObject oggettoIstanziato = Instantiate(oggetto_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x;
            oggettoIstanziato.transform.SetParent(Box_Oggetti.transform);

        }

        foreach (var x in Inventario.istanza.teoria)
        {
            GameObject oggettoIstanziato = Instantiate(teoria_Prefab, transform.position, Quaternion.identity);
            oggettoIstanziato.GetComponentInChildren<TextMeshProUGUI>().text = x;
            oggettoIstanziato.transform.SetParent(Box_Teoria.transform);

        }
    }

    public void AddItem(string clueText, Clue.ClueType clueType)
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
    }

}
