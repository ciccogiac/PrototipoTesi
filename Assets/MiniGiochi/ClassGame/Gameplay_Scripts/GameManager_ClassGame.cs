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

    [SerializeField] private List<string> methods;
    [SerializeField] private List<string> attributes;

    [SerializeField] private string className;

    [SerializeField] public Dictionary<string, (bool ,List<string>)> coppie;

    [SerializeField] CompilationResult_UI compilationResult_UI;


    [SerializeField] float secondToShowGameover = 5f;
    [SerializeField] GameObject canvas_Gameover;


    public float tempoIniziale = 20f; // Tempo iniziale del timer in secondi
    private float tempoRimanente; // Tempo rimanente nel timer
    [SerializeField] TextMeshProUGUI text_timer;
    [SerializeField] bool useTimer = true;

    [SerializeField] GameObject levels;

    // Start is called before the first frame update
    void Start()
    {
        //Legge i valori dei datipersistenti e li assegna alle variabili locali della scena
        if (DatiPersistenti.istanza != null) { 
            className = DatiPersistenti.istanza.className;
            tempoIniziale = DatiPersistenti.istanza.timer;
            methods = DatiPersistenti.istanza.methods;
            attributes = DatiPersistenti.istanza.attributes;
            coppie = DatiPersistenti.istanza.coppie;
        }

        LoadLevel();

        attributesConnections = new List<Attribute_Connection>();   //crea nuova lista di attribui vuota
        text_className.text = className;                            //imposta il nome della classe


        //istanzia gli oggetti metodi e li inizializza
        for (int i = 0; i < methods.Count; i++)
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
        for (int i = 0; i < attributes.Count; i++)
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

        tempoRimanente = tempoIniziale; // Imposta il tempo rimanente al valore iniziale

    }

    private void LoadLevel()
    {
        Transform livelloTrovato = levels.transform.Find(className);

        if (livelloTrovato != null)
        {
            // Fai qualcosa con l'oggetto trovato
            Debug.Log("Livello trovato: " + livelloTrovato.name);
            livelloTrovato.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Nessun livello trovato con il nome specificato.");
        }
    }

    void Update()
    {
        if (useTimer) { TimerCount(); }
    }

    private void TimerCount()
    {


        // Controlla se il timer � scaduto
        if (tempoRimanente < 0f)
        {
            tempoRimanente = 0f; // Imposta il timer a zero per evitare valori negativi
            text_timer.text = "0";
            TimeOut(); // Esegui qualche azione quando il timer scade
        }

        else
        {
            // Decrementa il timer
            text_timer.text = Mathf.FloorToInt(tempoRimanente).ToString();
            tempoRimanente -= Time.deltaTime;
            
        }
    }

    void TimeOut()
    {
        // Puoi eseguire azioni specifiche quando il timer raggiunge zero
        GameOver();
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

        //Bisogna gestire cosa si ritorna come oggetto e impostare il valore di visibilit� degli attributi definito nel minigioco

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


    public void GameOver()
    {
        StartCoroutine(GameoverCoroutine(secondToShowGameover));
    }

    IEnumerator GameoverCoroutine(float time)
    {
        canvas_Gameover.SetActive(true);
        yield return new WaitForSeconds(time);

        string nomeScenaCorrente = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeScenaCorrente);
    }
}