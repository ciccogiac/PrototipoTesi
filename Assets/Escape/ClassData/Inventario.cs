using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario istanza;

    //Rappresenta l'inventario temporaneo in una scena di escape . I dati verranno poi passati ai datipersitenti per una scena di minigioco(Nella classe ClassGameStarter) o di escape.
    public List<(string,string)> teoria = new List<(string, string)>();
    public List<(string,string)> methods = new List<(string, string)>();
    public List<(string, string)> attributes = new List<(string, string)>();
    //public List<string> classi;
    public List<ClassValue> classi;
    public List<OggettoEscapeValue> oggetti;

    public List<(string,string)> methodsAttributesUsed = new List<(string, string)>();
    public List<OggettoEscapeValue> oggettiUsed;

    public InventoryLoad inventoryLoad;

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

    public bool IsClueUsed(Clue clue)
    {

        if(clue.clueType == Clue.ClueType.Attributo || clue.clueType == Clue.ClueType.Metodo)
        {
            if (methodsAttributesUsed.Find(x => x.Item1 == clue.clueName) != (null,null)) { return true; }
        }

        if (clue.clueType == Clue.ClueType.Oggetto )
        {
            OggettoEscapeValue o = oggettiUsed.Find(x => x.objectName == clue.clueName);
            if (o != null) { return true; }
        }

        return false;
    }

    public bool IsCluePickedUp(Clue clue)
    {
        switch (clue.clueType)
        {
            case Clue.ClueType.Teoria:
                return (teoria.Find(x => x.Item1 == clue.clueName)!= (null,null)) ? true : false;
                //return teoria.Contains(clue.clueName);

            case Clue.ClueType.Attributo:
                return (attributes.Find(x => x.Item1 == clue.clueName) != (null, null)) ? true : false;

            case Clue.ClueType.Metodo:
                return (methods.Find(x => x.Item1 == clue.clueName) != (null, null)) ? true : false;


            case Clue.ClueType.Classe:
                ClassValue c = classi.Find(x => x.className == clue.clueName);
                return c!=null;
                

                
            case Clue.ClueType.Oggetto:
                OggettoEscapeValue o = oggetti.Find(x => x.objectName == clue.clueName);
                return o!=null;
                
        }

        return true;
    }

    public void PickUpClue(Clue clue)
    {
        switch (clue.clueType)
        {
            case Clue.ClueType.Teoria:
                teoria.Add((clue.clueName,clue.clueDescription));
                inventoryLoad.AddItem(clue.clueName, clue.clueDescription, clue.clueType);
                break;

            case Clue.ClueType.Attributo:
                attributes.Add((clue.clueName,clue.clueDescription));
                inventoryLoad.AddItem(clue.clueName, clue.clueDescription,clue.clueType);
                break;

            case Clue.ClueType.Metodo:
                methods.Add((clue.clueName, clue.clueDescription));
                inventoryLoad.AddItem(clue.clueName, clue.clueDescription, clue.clueType);
                break;

                
            case Clue.ClueType.Classe:
                classi.Add(clue.gameObject.GetComponent<ClasseEscape>().classValue);
                inventoryLoad.AddItem(clue.clueName, clue.clueDescription, clue.clueType);
                break;
                

                /*
            case Clue.ClueType.Oggetto:
                oggetti.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;
                */
        }
    }

    public void PickUpObject(OggettoEscapeValue oggetto)
    {
        oggetti.Add(oggetto);
        inventoryLoad.AddItem(oggetto.objectName, "", Clue.ClueType.Oggetto);
    }
}
