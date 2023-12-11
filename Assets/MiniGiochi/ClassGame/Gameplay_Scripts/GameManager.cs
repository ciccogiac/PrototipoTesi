using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject method_prefab;
    [SerializeField] GameObject attribute_prefab;

    [SerializeField] GameObject VerticalBox_Methods;
    [SerializeField] GameObject VerticalBox_Attributes;
    private List<GameObject> attributes_GO;

    [SerializeField] TextMeshProUGUI text_className;

    [SerializeField] private string[] methods;
    [SerializeField] private string[] attributes;

    [SerializeField] private string className;

    //[SerializeField] public HashSet<string[]> coppie;
    [SerializeField] public Dictionary<string, string> coppie;

    
    // Start is called before the first frame update
    void Start()
    {
        className = PlayerPrefs.GetString("ClassName", "Default");
        methods = DatiPersistenti.istanza.methods;
        attributes = DatiPersistenti.istanza.attributes;
        coppie = DatiPersistenti.istanza.coppie;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        attributes_GO = new List<GameObject>();
        text_className.text = className;

        for (int i = 0; i < methods.Length; i++)
        {
            GameObject oggettoIstanziato = Instantiate(method_prefab, transform.position, Quaternion.identity);
            Method_initializer mi = oggettoIstanziato.GetComponent<Method_initializer>();
            if (mi != null)
            {
                mi.method_name = methods[i];
                mi.initialize();
            }
            oggettoIstanziato.transform.SetParent(VerticalBox_Methods.transform);
        }

        for (int i = 0; i < attributes.Length; i++)
        {
            GameObject oggettoIstanziato = Instantiate(attribute_prefab, transform.position, Quaternion.identity);
            attributes_GO.Add(oggettoIstanziato);
            Attribute_initializer mi = oggettoIstanziato.GetComponent<Attribute_initializer>();
            if (mi != null)
            {
                mi.attribute_name = attributes[i];
                mi.initialize();
            }
            oggettoIstanziato.transform.SetParent(VerticalBox_Attributes.transform);
        }

       
        foreach (var coppia in coppie)
        {
            Debug.Log("Chiave: " + coppia.Key + ", Valore: " + coppia.Value);
        }
    }


    public void Compile()
    {
        foreach (var coppia in coppie)
        {
            foreach (var a in attributes_GO)
            {
                if (coppia.Key == a.GetComponentInChildren<Attribute_Connection>().attribute_name)
                    if (coppia.Value != a.GetComponentInChildren<Attribute_Connection>().method_name)
                        { Debug.Log("Errore di Compilazione");
                        return;
                          }

            }
        }

        Debug.Log("Compilazione avvenuta correttamente");
        SceneManager.LoadScene("Playground");
    }
}
