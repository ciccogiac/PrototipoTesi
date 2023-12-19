using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatiPersistenti : MonoBehaviour
{
    public static DatiPersistenti istanza;
    public string className;
    public string[] methods;
    public string[] attributes;
    public Dictionary<string, (bool visibility,List<string>)> coppie;

    public Vector3 lastCharacterEscapePosition;

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
