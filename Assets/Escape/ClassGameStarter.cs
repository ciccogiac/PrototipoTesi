using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : MonoBehaviour
{
    [SerializeField] string className;
    [SerializeField] GameObject text_active;
    public StarterAssetsInputs _input;

    [SerializeField] public Dictionary<string, (bool,List<string>)> coppie ;
    [SerializeField] Inventario inventary;

    private ClassDictionary classDictionary;
    

    //L'interazione con l'oggetto fa partirte il minigioco delle classi.Si crea il rispettivo dizionario per il tipo di classe , e si passano i dati attuali tramite i datipersistenti.

    private void Start()
    {
        classDictionary = FindObjectOfType<ClassDictionary>();
        coppie = classDictionary.FindClass(className);

        if (coppie != null)
            StampaDizionario(coppie);
        else
            Debug.Log("Classe non trovata nel dizionario");
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text_active.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text_active.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_input.interact == true)
            {
                _input.interact = false;

                PlayerPrefs.SetString("ClassName", className);
                DatiPersistenti.istanza.methods = inventary.methods;
                DatiPersistenti.istanza.attributes = inventary.attributes;
                DatiPersistenti.istanza.coppie = coppie;

                DatiPersistenti.istanza.lastCharacterEscapePosition = other.transform.position;

                SceneManager.LoadScene("ClassGame");
            }
        }
    }
}
