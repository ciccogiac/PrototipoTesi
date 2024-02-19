using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatiPersistenti : MonoBehaviour
{
    public static DatiPersistenti istanza;

    public Vector3 lastCharacterEscapePosition;
    public Quaternion lastCharacterEscapeRotation;

    public HashSet<int> methodsListeners = new HashSet<int>();

    public bool isFirstSceneOpening = true;

    public int sceneIndex;

    [Header("ClassGame")]
    public string className;
    public Dictionary<string, (bool visibility,List<Method>)> coppie;

    [Header("ObjectGame")]
    public bool isObjectToPrint=false;
    public string objectName;
    public List<Attribute> attributesValues;

    //Rappresenta l'istanza unica dei dati che si condividono tra le varie scene. Tra cui i valori dell'inventario , il dizionario di ogni minigioco classe, ecc..

    void Awake()
    {
        // Assicurati che esista una sola istanza di questo oggetto
        if (istanza == null)
        {
            istanza = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SvuotaDatiPersistenti()
    {
        methodsListeners = new HashSet<int>();
        isFirstSceneOpening = true;
    }
}
