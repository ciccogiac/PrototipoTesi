using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class Printer3DController : Interactable
{
    [SerializeField] GameObject canvas_ObjectGameInterface;
    
    [SerializeField] private GameObject Glows;
    [SerializeField] private GameObject Lights;
    [SerializeField] private GameObject ToSpawn;
    [SerializeField] private Transform SpawnPos1;
    [SerializeField] private Transform SpawnPos2;
    [SerializeField] private Transform SpawnPos3;
    
    private void Start()
    {
        if (DatiPersistenti.istanza.isObjectToPrint)
        {
            DatiPersistenti.istanza.isObjectToPrint = false;
            StartCoroutine(Print());
        }
    }

    private IEnumerator Print()
    {
        gameObject.tag = "Untagged";
        Glows.SetActive(true);
        var toScale1 = new Vector3(0.2f, 0.2f, 0.2f);
        var toScale2 = new Vector3(0.6f, 0.6f, 0.6f);
        var timer = 0f;
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            Glows.transform.localScale = Vector3.Lerp(Vector3.zero, toScale1, timer / 0.8f);
            yield return null;
        }
        Glows.transform.localScale = toScale1;
        var pos = Glows.transform.position;
        pos = new Vector3(pos.x, pos.y, pos.z);


        var o = FindObjectOfType<ClassDictionary>().GetClassPrefab(DatiPersistenti.istanza.className);
        ToSpawn = o.Item1;
        var objectSpawned = Instantiate(ToSpawn, pos, Quaternion.identity);
        OggettoEscape oggetto = null;   //aggiunta
        if (ToSpawn != null)
        {
            Clue clue = objectSpawned.GetComponent<Clue>();
            clue.clueType = Clue.ClueType.Oggetto;
            clue.clueName= DatiPersistenti.istanza.objectName;
            clue.clueDescription = o.Item2;


            oggetto = objectSpawned.GetComponent<OggettoEscape>();
            oggetto.oggettoEscapeValue.isMadeByPrinter = true;
            oggetto.tag = "Untagged";   //aggiunta
            oggetto.oggettoEscapeValue.classPrefab = ToSpawn;
            oggetto.oggettoEscapeValue.objectName = DatiPersistenti.istanza.objectName;
            oggetto.oggettoEscapeValue.className = DatiPersistenti.istanza.className;
            oggetto.oggettoEscapeValue.attributes = DatiPersistenti.istanza.attributesValues;
            oggetto.oggettoEscapeValue.methods = GetObjectMethods();

        }





        var objectScale = objectSpawned.transform.localScale;
        objectSpawned.transform.localScale = Vector3.zero;
        timer = 0f;
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            objectSpawned.transform.localScale = Vector3.Lerp(Vector3.zero, objectScale, timer / 0.8f);
            yield return null;
        }
        objectSpawned.transform.localScale = objectScale;
        timer = 0f;
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            Glows.transform.localScale = Vector3.Lerp(toScale1, Vector3.zero, timer / 0.8f);
            yield return null;
        }
        Glows.transform.localScale = Vector3.zero;
        Glows.SetActive(false);
        timer = 0f;
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            objectSpawned.transform.localScale = Vector3.Lerp(objectScale, Vector3.zero, timer / 0.8f);
            yield return null;
        }
        Lights.SetActive(true);
        objectSpawned.transform.position = SpawnPos1.position;
        yield return new WaitForSeconds(0.5f);
        timer = 0f;
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            objectSpawned.transform.localScale = Vector3.Lerp(Vector3.zero, objectScale, timer / 0.8f);
            Lights.transform.localScale = Vector3.Lerp(toScale2, Vector3.zero, timer / 0.8f);
            yield return null;
        }
        Lights.SetActive(false);
        Lights.transform.localScale = toScale2;
        objectSpawned.transform.localScale = objectScale;
        var alembic = GetComponent<AlembicStreamPlayer>();
        while (alembic.CurrentTime < 2f)
        {
            alembic.CurrentTime += Time.deltaTime;
            objectSpawned.transform.position =
                Vector3.Lerp(SpawnPos1.position, SpawnPos2.position, alembic.CurrentTime / 2f);
            yield return null;
        }
        timer = 0f;
        var isBack = false;
        IEnumerator TakeBackRullo()
        {
            while (alembic.CurrentTime > 0f)
            {
                alembic.CurrentTime -= Time.deltaTime;
                yield return null;
            }
            alembic.CurrentTime = 0f;
            isBack = true;
        }
        StartCoroutine(TakeBackRullo());
        while (timer < 0.8f)
        {
            timer += Time.deltaTime;
            objectSpawned.transform.position = Vector3.Lerp(SpawnPos2.position, SpawnPos3.position, timer / 0.8f);
            yield return null;
        }
        objectSpawned.transform.position = SpawnPos3.position;
        yield return new WaitUntil(() => isBack);

        //gameObject.tag = "Interactable"; //Aggiunte
        if (ToSpawn != null)
            objectSpawned.tag = "Interactable";


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

        /*
        foreach (var tupla in listaDiTupleSenzaDuplicati)
        {
            Debug.Log("MethodName: " + tupla.method.methodName +" MethodType: " + tupla.method.methodType + " Attributes : " + string.Join(", ", tupla.attributes));
        }
        */
        Methos m_pickup = new Methos(new Method("PickUpObject", Method.MethodType.pickUp), new List<string> { });
        listaDiTupleSenzaDuplicati.Add(m_pickup);
        Methos m_destroy = new Methos(new Method("DestroyObject", Method.MethodType.destroy), new List<string> { });
        listaDiTupleSenzaDuplicati.Add(m_destroy);

        return listaDiTupleSenzaDuplicati;
    }

    public override void Interact()
    {
        canvas_ObjectGameInterface.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
