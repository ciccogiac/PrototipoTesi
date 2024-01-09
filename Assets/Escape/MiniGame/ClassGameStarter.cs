using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : Interactable
{
    [SerializeField] string className;
    [SerializeField] float timer=60f;
    

    [SerializeField] public Dictionary<string, (bool,List<string>)> coppie ;

    private ClassDictionary classDictionary;

    [SerializeField] GameObject player;

    [SerializeField] GameObject canvas_ClassGameInterface;

    //L'interazione con l'oggetto fa partirte il minigioco delle classi.Si crea il rispettivo dizionario per il tipo di classe , e si passano i dati attuali tramite i datipersistenti.

    private void Start()
    {
        classDictionary = FindObjectOfType<ClassDictionary>();
        coppie = classDictionary.FindClass(className);

        /*
        if (coppie != null)
            StampaDizionario(coppie);
        else
            Debug.Log("Classe non trovata nel dizionario");
        */
    }

    private void StampaDizionario(Dictionary<string, (bool ,List<string>)> dizionario)
    {
        foreach (var coppia in dizionario)
        {
            string s="";
            coppia.Value.Item2.ForEach(x => s= s + " " + x);
            string v = "";
            v = coppia.Value.Item1 ? "public" : "private";
            Debug.Log("Attributo : " + coppia.Key + " Visibilità : "+ v +" Metodi : " + s);
        }
    }
    


    
    override public void Interact()
    {
                //PlayerPrefs.SetString("ClassName", className);
                DatiPersistenti.istanza.className = className;
                DatiPersistenti.istanza.timer = timer;
                DatiPersistenti.istanza.coppie = coppie;

                DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;

                //SceneManager.LoadScene("ClassGame");

                canvas_ClassGameInterface.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;


    }
    
}
