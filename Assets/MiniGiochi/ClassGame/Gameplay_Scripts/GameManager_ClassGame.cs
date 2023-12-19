using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_ClassGame : MonoBehaviour
{
    /*Gestisce i dati principali e funzioni del minigioco.
     Si caricano i dati dai dati persistenti precedenti, e si aggiornano i verticalbox di metodi e attributi.
     */
    [SerializeField] GameObject method_prefab;
    [SerializeField] GameObject attribute_prefab;

    [SerializeField] GameObject VerticalBox_Methods;
    [SerializeField] GameObject VerticalBox_Attributes;

    private List<Attribute_Connection> attributesConnections; //lista con tutti gli attributiConnections creati

    [SerializeField] TextMeshProUGUI text_className;

    [SerializeField] private string[] methods;
    [SerializeField] private string[] attributes;

    [SerializeField] private string className;

    [SerializeField] public Dictionary<string, (bool ,List<string>)> coppie;

    [SerializeField] CompilationResult_UI compilationResult_UI;

    // Start is called before the first frame update
    void Start()
    {
        //Legge i valori dei datipersistenti e li assegna alle variabili locali della scena
        if (DatiPersistenti.istanza != null) { 
            className = DatiPersistenti.istanza.className;
            methods = DatiPersistenti.istanza.methods;
            attributes = DatiPersistenti.istanza.attributes;
            coppie = DatiPersistenti.istanza.coppie;
        }

        attributesConnections = new List<Attribute_Connection>();   //crea nuova lista di attribui vuota
        text_className.text = className;                            //imposta il nome della classe


        //istanzia gli oggetti metodi e li inizializza
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

        //istanzia gli oggetti attribbuti e li inizializza
        for (int i = 0; i < attributes.Length; i++)
        { 
            GameObject oggettoIstanziato = Instantiate(attribute_prefab, transform.position, Quaternion.identity);
            attributesConnections.Add(oggettoIstanziato.GetComponentInChildren<Attribute_Connection>());
            Attribute_initializer mi = oggettoIstanziato.GetComponent<Attribute_initializer>();
            if (mi != null)
            {
                mi.attribute_name = attributes[i];
                mi.initialize();
            }
            oggettoIstanziato.transform.SetParent(VerticalBox_Attributes.transform);
        }

        //rende il cursore visibile e bloccati ai limite della finestra , per il minigioco 2D
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        

    }


    //Funzione che gestisce la compilazione del minigioco , verifica per ogni valore del dizionario se per ogni attrbuto sono stati colllegati tutti i metodi necessari.
    //In caso positivo apre la scena di escape precedente
    public void Compile()
    {
        foreach (var coppia in coppie)
        {
            foreach (var a in attributesConnections)
            {
                if (coppia.Key == a.attribute_name)
                    //if (coppia.Value != a.method_name) coppia.Value.Contains(a.method_name) != a.method_name
                    if(!VerifyMethodList(coppia.Value.Item2 , a.method_names))
                    {
                        compilationResult_UI.Compile(false);
                        return;
                          }

            }
        }

        //Bisogna gestire cosa si ritorna come oggetto e impostare il valore di visibilità degli attributi definito nel minigioco

        compilationResult_UI.Compile(true);
    }

    private bool VerifyMethodList(List<string> dictionary_list , List<string> attribute_list)
    {

        foreach(var x in dictionary_list) { if (!attribute_list.Contains(x)) return false; }
        foreach (var y in attribute_list) { if (!dictionary_list.Contains(y)) return false; }

        return true;
    }

    public void ReturnToescape(bool is_game_won)
    {
        Cursor.visible = false;
        if (is_game_won) { SceneManager.LoadScene("Playground"); }
        else { SceneManager.LoadScene("Playground"); }
        
    }
}
