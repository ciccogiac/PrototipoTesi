using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class DatiPersistenti : MonoBehaviour
{

    public static string IDCurrentSessionEsperimento;
    public static string LOGFilePath;
    
    public static DatiPersistenti istanza;

    public Vector3 lastCharacterEscapePosition;
    public Quaternion lastCharacterEscapeRotation;

    public HashSet<int> methodsListeners = new HashSet<int>();
    public HashSet<int> dialogUsed = new HashSet<int>();

    public bool isFirstSceneOpening = true;

    public int sceneIndex;
    public int hintCounter;

    [Header("ClassGame")]
    public string className;
    public Dictionary<string, (bool visibility,List<Method>)> coppie;
    public bool isNewClassCreated = false;
    public bool isTutorialStarted_CG = false;

    [Header("ObjectGame")]
    public bool isObjectToPrint=false;
    public string objectName;
    public List<Attribute> attributesValues;
    public bool isTutorialStarted_OG = false;

    //Rappresenta l'istanza unica dei dati che si condividono tra le varie scene. Tra cui i valori dell'inventario , il dizionario di ogni minigioco classe, ecc..

    void Awake()
    {
        // Assicurati che esista una sola istanza di questo oggetto
        if (istanza == null)
        {
            istanza = this;
            InitLog();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void InitLog()
    {
        LOGFilePath = Path.Combine(Application.dataPath, "BuildLogs", $"{IDCurrentSessionEsperimento}.escapeia");
        Directory.CreateDirectory(Path.GetDirectoryName(LOGFilePath)!);
        LogMessage("Log iniziato");
    }

    public static void LogMessage(string message)
    {
        File.AppendAllText(LOGFilePath, DateTime.Now + "\t" + message + Environment.NewLine);
        istanza.StartCoroutine(ServerUploader.ServerUploader.UploadToServer());
    }

    public void SvuotaDatiPersistenti()
    {
        methodsListeners = new HashSet<int>();
        dialogUsed = new HashSet<int>();
        isFirstSceneOpening = true;
    }
}
