using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] public Dictionary<string, (bool ,List<Method>)> coppie;

    [SerializeField] CompilationResult_UI compilationResult_UI;


    [SerializeField] float secondToShowGameover = 5f;
    [SerializeField] GameObject canvas_Gameover;
    [SerializeField] GameObject canvas_Game;


    public float tempoIniziale = 20f; // Tempo iniziale del timer in secondi
    private float tempoRimanente; // Tempo rimanente nel timer
    [SerializeField] TextMeshProUGUI text_timer;
    [SerializeField] bool useTimer = true;

    [SerializeField] GameObject levels;

    [SerializeField] GameObject buttonCompile;
    [SerializeField] GameObject boxCompilation;

    private bool isVisibilityLevel;
    private bool isPrivateNonVisibilityLevel;

    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] GameObject upBox;
    [SerializeField] Image backgroundPlayImage;

    private Transform livelloTrovato;

    [SerializeField] Gradient[] connection_Colors;
    private int lineColor_Counter = 0;

    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip gameoverAudio;
    [SerializeField] AudioClip compileAudio;
    [SerializeField] AudioClip wrongCompileAudio;
    [SerializeField] AudioClip closeAudio;
    [SerializeField] AudioClip clickPCbutton_Audio;

    [SerializeField] TextMeshProUGUI progettoClasseText;
    [SerializeField] GameObject textClass;
    private bool progettoClasse_State = false;
    [SerializeField] Button progettoClasse_Button;
    [SerializeField] Color pc_color1;
    [SerializeField] Color pc_color2;

    // Start is called before the first frame update
    void Start()
    {
        //Legge i valori dei datipersistenti e li assegna alle variabili locali della scena
        if (DatiPersistenti.istanza != null) { 
            className = DatiPersistenti.istanza.className;
            coppie = DatiPersistenti.istanza.coppie;
            
        }

        if (Inventario.istanza != null)
        {

            methods = Inventario.istanza.methods.Select(tuple => tuple.Item1).ToList(); ;
            attributes = Inventario.istanza.attributes.Select(tuple => tuple.Item1).ToList();

            string d = Inventario.istanza.ProgettiClasse.Find(x => x.Item1 == className).Item2;
            progettoClasseText.text = d;
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
            oggettoIstanziato.transform.localScale = Vector3.one;
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
                mi.initialize(isVisibilityLevel, isPrivateNonVisibilityLevel);
            }
            oggettoIstanziato.transform.SetParent(VerticalBox_Attributes.transform);
            oggettoIstanziato.transform.localScale = Vector3.one;
        }

        //rende il cursore visibile e bloccati ai limite della finestra , per il minigioco 2D
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        tempoRimanente = tempoIniziale; // Imposta il tempo rimanente al valore iniziale

        //ActivateTutorial();

        if (className == "Valvola" && !DatiPersistenti.istanza.isTutorialStarted_CG)
        {
            DatiPersistenti.istanza.isTutorialStarted_CG = true;
            ActivateTutorial();
        }

    }

    public void SetProgettoClassePanel()
    {
        progettoClasse_Button.interactable = false;

        audio.clip = clickPCbutton_Audio;
        audio.Play();

        progettoClasse_State = !progettoClasse_State;

        progettoClasseText.gameObject.SetActive(progettoClasse_State);
        text_className.gameObject.SetActive(!progettoClasse_State);
        textClass.SetActive(!progettoClasse_State);

        progettoClasse_Button.image.color = progettoClasse_State ? pc_color2 : pc_color1;


        progettoClasse_Button.interactable = true;
    }

    public Gradient GetLineColor()
    {
        if (lineColor_Counter >= connection_Colors.Length)
            lineColor_Counter = 0;

        return connection_Colors[lineColor_Counter++];
    }

    private void ActivateTutorial()
    {
        useTimer = false;

        livelloTrovato.gameObject.SetActive(false);
        //livelloInventoryTrovato.gameObject.SetActive(false);
        VerticalBox_Methods.transform.parent.gameObject.SetActive(false);
        VerticalBox_Attributes.transform.parent.gameObject.SetActive(false);
        upBox.SetActive(false);
        backgroundPlayImage.enabled = false;
        tutorialCanvas.SetActive(true);
    }

    public void EndTutorial()
    {
        tutorialCanvas.SetActive(false);
        livelloTrovato.gameObject.SetActive(true);
        //livelloInventoryTrovato.gameObject.SetActive(true);
        VerticalBox_Methods.transform.parent.gameObject.SetActive(true);
        VerticalBox_Attributes.transform.parent.gameObject.SetActive(true);
        upBox.SetActive(true);
        backgroundPlayImage.enabled = true;
        useTimer = true;

    }

    private void LoadLevel()
    {
        livelloTrovato = levels.transform.Find(className);
        

        if (livelloTrovato != null)
        {
            // Fai qualcosa con l'oggetto trovato
            //Debug.Log("Livello trovato: " + livelloTrovato.name);
            LevelTimer level = livelloTrovato.GetComponent<LevelTimer>();
            tempoIniziale = level.timer;
            isVisibilityLevel = level.isVisibilityLevel;
            isPrivateNonVisibilityLevel = level.isPrivateNonVisibilityLevel;
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


        // Controlla se il timer è scaduto
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
                    if(!VerifyMethodList(coppia.Value.Item2, a.method_names))
                    {
                        buttonCompile.SetActive(false);
                        boxCompilation.SetActive(true);
                        compilationResult_UI.Compile(false);

                        audio.clip = wrongCompileAudio;
                        audio.Play();

                        return;
                          }

            }
        }

        //Bisogna gestire cosa si ritorna come oggetto e impostare il valore di visibilità degli attributi definito nel minigioco

        useTimer = false;

        buttonCompile.SetActive(false);
        boxCompilation.SetActive(true);
        compilationResult_UI.Compile(true);

        audio.clip = compileAudio;
        audio.Play();
    }

    private bool VerifyMethodList(List<Method> dictionary_list , List<string> attribute_list)
    {
        List<string> methodsList = new List<string>();
        foreach(var x in dictionary_list) { methodsList.Add(x.methodName); }

        foreach(var x in methodsList) { if (!attribute_list.Contains(x)) return false; }
        foreach (var y in attribute_list) { if (!methodsList.Contains(y)) return false; }

        return true;
    }

    public ClassValue FromDictionaryToClassValue()
    {
        ClassValue classValue = new ClassValue();
        classValue.className = className;

        List<AttributeValue> l = new List<AttributeValue>();
        foreach( var x in coppie)
        {
            AttributeValue a = new AttributeValue();
            a.attribute = x.Key;
            a.visibility = x.Value.Item1;
            a.methods = x.Value.Item2;

            l.Add(a);
        }

        classValue.attributes = l;
        return classValue;

    }
    public void ReturnToescape(bool is_game_won)
    {
        Cursor.visible = false;
        if (is_game_won) {
            //gestire eventuali attribute aggiuntivi collegati a metodi erronemanete
            foreach (var a in attributesConnections)
            {
                if (!coppie.ContainsKey(a.attribute_name) && a.method_names.Count!=0)
                {
                    string attributo = a.attribute_name;
                    bool attributeVisibility = a.is_public;

                    List<string> l = a.method_names;
                    List<Method> lm = new List<Method>();

                    foreach(var m in l)
                    {
                        Method met = new Method(m, Method.MethodType.getter);
                        lm.Add(met);
                    }

                    coppie.Add(attributo,(attributeVisibility,lm));
                    
                }
            }

                //Gestire l'eventuale valore private/public degli attributi se utilizzato in seguito
                foreach (var a in attributesConnections)
            {
                if (coppie.ContainsKey(a.attribute_name))
                {
                    bool attributeVisibility = a.is_public;
                    List<Method> l = coppie[a.attribute_name].Item2;
                    coppie[a.attribute_name] = (attributeVisibility, l);
                }
            }

            //Creare la classe e aggiungerla all'inventario
            Inventario.istanza.classi.Add(FromDictionaryToClassValue());
            //Eliminare dall'inventario metodi e attributi usati nelle classe
            foreach(var coppia in coppie)
            {
                foreach(var method in coppia.Value.Item2)
                {
                    (string, string) b = Inventario.istanza.methods.Find(x => x.Item1 == method.methodName);
                    if (b != (null, null))
                    {
                        Inventario.istanza.methodsAttributesUsed.Add(b);
                        Inventario.istanza.methods.Remove(b);
                    }
                    
                }

                (string,string) a=Inventario.istanza.attributes.Find(x => x.Item1 == coppia.Key);
                if (a != (null, null))
                {
                    Inventario.istanza.methodsAttributesUsed.Add(a);
                    Inventario.istanza.attributes.Remove(a);
                    
                }
            }

            DatiPersistenti.istanza.isNewClassCreated = true;
            SceneManager.LoadScene(DatiPersistenti.istanza.sceneIndex);
        }
        
        
        else {
            audio.clip = closeAudio;
            audio.Play(); 
            SceneManager.LoadScene(DatiPersistenti.istanza.sceneIndex); }

    }


    public void GameOver()
    {
        StartCoroutine(GameoverCoroutine(secondToShowGameover));
    }

    IEnumerator GameoverCoroutine(float time)
    {
        audio.clip = gameoverAudio;
        audio.Play();

        canvas_Game.SetActive(false);
        canvas_Gameover.SetActive(true);
        yield return new WaitForSeconds(time);

        string nomeScenaCorrente = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeScenaCorrente);
    }
}
