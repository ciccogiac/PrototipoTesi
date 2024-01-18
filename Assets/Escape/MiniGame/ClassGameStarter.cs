using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : Interactable
{
    [SerializeField] string className;
    [SerializeField] float timer=60f;
    

    [SerializeField] public Dictionary<string, (bool,List<Method>)> coppie ;

    //private ClassDictionary classDictionary;

    [SerializeField] GameObject player;

    [SerializeField] GameObject canvas_ClassGameInterface;

    //L'interazione con l'oggetto fa partirte il minigioco delle classi.Si crea il rispettivo dizionario per il tipo di classe , e si passano i dati attuali tramite i datipersistenti.

    private void Start()
    {

        coppie = FindObjectOfType<ClassDictionary>().FindClass(className);

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
