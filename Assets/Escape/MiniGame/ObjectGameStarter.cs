using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGameStarter : Interactable
{
    [SerializeField] GameObject canvas_ObjectGameInterface;

    public GameObject oggettoDaIstanzare;
    public Transform printingPosition;

    private void Start()
    {
        
        DatiPersistenti.istanza.isObjectToPrint = true;
        DatiPersistenti.istanza.objectName= "studentePOLI";
        DatiPersistenti.istanza.className = "Studente";
        DatiPersistenti.istanza.coppie = FindObjectOfType<ClassDictionary>().FindClass(DatiPersistenti.istanza.className);
        List<(string, string)> l = new List<(string, string)> { ("Età", "25"), ("Nome", "Ernesto"), ("StudenteLaureato", "False") };
        DatiPersistenti.istanza.attributesValues = l;
        

        if (DatiPersistenti.istanza.isObjectToPrint)
        {
            DatiPersistenti.istanza.isObjectToPrint=false;
            PrintObject();
        }
    }

    private void PrintObject()
    {
        string objectName = DatiPersistenti.istanza.objectName;
        GameObject nuovoOggetto = Instantiate(oggettoDaIstanzare, printingPosition.position, transform.rotation);
        OggettoEscape oggetto = nuovoOggetto.GetComponent<OggettoEscape>();
        oggetto.objectName = DatiPersistenti.istanza.objectName; 
        oggetto.className = DatiPersistenti.istanza.className; 
        oggetto.attributes = DatiPersistenti.istanza.attributesValues;
        oggetto.methods = GetObjectMethods();

    }

    private List<(Method, List<string>)> GetObjectMethods()
    {


        Dictionary<string, (bool, List<Method>)> dizionarioOriginale = DatiPersistenti.istanza.coppie;

       // List<(Method, List<string>)> listaDiTuple = dizionarioOriginale.Select(kvp => (kvp.Value.Item2.FirstOrDefault(), dizionarioOriginale.Where(item => item.Value.Item2.Any(m => m.methodName == kvp.Value.Item2.First().methodName)).Select(item => item.Key).ToList())).ToList();

        List<(Method, List<string>)> listaDiTuple = dizionarioOriginale
           .SelectMany(kvp => kvp.Value.Item2.Select(m => (m, kvp.Key)))
           .GroupBy(tuple => tuple.Item1)
           .Select(group => (group.Key, group.Select(tuple => tuple.Item2).ToList()))
           .ToList();

        // Elimina duplicati di tuple mantenendo solo una tupla per ogni metodo
        HashSet<string> metodiGiaVisti = new HashSet<string>();
        List<(Method, List<string>)> listaDiTupleSenzaDuplicati = new List<(Method, List<string>)>();

        foreach (var tupla in listaDiTuple)
        {
            if (metodiGiaVisti.Add(tupla.Item1.methodName))
            {
                listaDiTupleSenzaDuplicati.Add(tupla);
            }
            else
            {
                listaDiTupleSenzaDuplicati.Find(x => x.Item1.methodName == tupla.Item1.methodName).Item2.Add(tupla.Item2[0]);
            }
        }

        foreach (var tupla in listaDiTupleSenzaDuplicati)
        {
            Debug.Log("MethodName: " + tupla.Item1.methodName +" MethodType: " + tupla.Item1.methodType + " Attributes : " + string.Join(", ", tupla.Item2));
        }
        listaDiTupleSenzaDuplicati.Add((new Method("PickUpObject", Method.MethodType.pickUp), new List<string> { }));
        listaDiTupleSenzaDuplicati.Add((new Method("DestroyObject", Method.MethodType.destroy), new List<string> { }));

        return listaDiTupleSenzaDuplicati;
    }

    override public void Interact()
    {
        canvas_ObjectGameInterface.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
}
