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

        if (DatiPersistenti.istanza.isObjectToPrint)
        {
            DatiPersistenti.istanza.isObjectToPrint=false;
            PrintObject();
        }
    }

    private void PrintObject()
    {
        (Mesh , Material[]) meshMaterials= FindObjectOfType<ClassDictionary>().GetMeshMaterials(DatiPersistenti.istanza.className);

        if (meshMaterials != (null, null))
        {

            string objectName = DatiPersistenti.istanza.objectName;
            GameObject nuovoOggetto = Instantiate(oggettoDaIstanzare, printingPosition.position, transform.rotation);
            OggettoEscape oggetto = nuovoOggetto.GetComponent<OggettoEscape>();
            oggetto.SetMeshMaterial(meshMaterials);
            oggetto.oggettoEscapeValue.isMadeByPrinter = true;
            oggetto.oggettoEscapeValue.objectName = DatiPersistenti.istanza.objectName;
            oggetto.oggettoEscapeValue.className = DatiPersistenti.istanza.className;
            oggetto.oggettoEscapeValue.attributes = DatiPersistenti.istanza.attributesValues;
            oggetto.oggettoEscapeValue.methods = GetObjectMethods();

        }


    }

    private List<Methos> GetObjectMethods()
    {
        ClassValue classValue = Inventario.istanza.classi.Find(x => x.className == DatiPersistenti.istanza.className );

        //Dictionary<string, (bool, List<Method>)> dizionarioOriginale = DatiPersistenti.istanza.coppie;
        Dictionary<string, (bool, List<Method>)> dizionarioOriginale = new Dictionary<string, (bool, List<Method>)>();
        if (classValue!= null)
        {
            foreach (var attribute in classValue.attributes)
            {
                dizionarioOriginale.Add(attribute.attribute, (attribute.visibility, attribute.methods));
            }
        }

       // List<(Method, List<string>)> listaDiTuple = dizionarioOriginale.Select(kvp => (kvp.Value.Item2.FirstOrDefault(), dizionarioOriginale.Where(item => item.Value.Item2.Any(m => m.methodName == kvp.Value.Item2.First().methodName)).Select(item => item.Key).ToList())).ToList();

        List<(Method, List<string>)> listaDiTuple = dizionarioOriginale
           .SelectMany(kvp => kvp.Value.Item2.Select(m => (m, kvp.Key)))
           .GroupBy(tuple => tuple.Item1)
           .Select(group => (group.Key, group.Select(tuple => tuple.Item2).ToList()))
           .ToList();

        // Elimina duplicati di tuple mantenendo solo una tupla per ogni metodo
        HashSet<string> metodiGiaVisti = new HashSet<string>();
        List<Methos> listaDiTupleSenzaDuplicati = new List<Methos>();

        foreach (var tupla in listaDiTuple)
        {
            if (metodiGiaVisti.Add(tupla.Item1.methodName))
            {
                Methos m = new Methos(tupla.Item1, tupla.Item2);
                listaDiTupleSenzaDuplicati.Add(m);
            }
            else
            {
                listaDiTupleSenzaDuplicati.Find(x => x.method.methodName == tupla.Item1.methodName).attributes.Add(tupla.Item2[0]);
            }
        }

        foreach (var tupla in listaDiTupleSenzaDuplicati)
        {
            Debug.Log("MethodName: " + tupla.method.methodName +" MethodType: " + tupla.method.methodType + " Attributes : " + string.Join(", ", tupla.attributes));
        }
        Methos m_pickup = new Methos(new Method("PickUpObject", Method.MethodType.pickUp), new List<string> { });
        listaDiTupleSenzaDuplicati.Add(m_pickup);
        Methos m_destroy = new Methos(new Method("DestroyObject", Method.MethodType.destroy), new List<string> { });
        listaDiTupleSenzaDuplicati.Add(m_destroy);

        return listaDiTupleSenzaDuplicati;
    }

    override public void Interact()
    {
        canvas_ObjectGameInterface.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
}
