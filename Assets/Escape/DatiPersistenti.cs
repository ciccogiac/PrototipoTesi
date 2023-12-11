using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatiPersistenti : MonoBehaviour
{
    public static DatiPersistenti istanza;
    public string[] methods;
    public string[] attributes;
    public Dictionary<string, string> coppie;

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
