using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatiPersistenti : MonoBehaviour
{
    public static DatiPersistenti istanza;

    public Vector3 lastCharacterEscapePosition;

    [Header("ClassGame")]
    public string className;
    public float timer;
    public Dictionary<string, (bool visibility,List<string>)> coppie;

    [Header("ObjectGame")]
    public bool isObjectToPrint=false;
    public string objectName;

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
}
