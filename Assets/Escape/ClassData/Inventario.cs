using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public static Inventario istanza;

    //Rappresenta l'inventario temporaneo in una scena di escape . I dati verranno poi passati ai datipersitenti per una scena di minigioco(Nella classe ClassGameStarter) o di escape.
    public List<string> teoria;
    public List<string> methods;
    public List<string> attributes;
    //public List<string> classi;
    public List<ClassValue> classi;
    public List<OggettoEscape> oggetti;

    public List<string> methodsAttributesUsed;

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
            if (methodsAttributesUsed.Contains(clue.clueName)) { return true; }
        }

        return false;
    }

    public bool IsCluePickedUp(Clue clue)
    {
        switch (clue.clueType)
        {
            case Clue.ClueType.Teoria:
                return teoria.Contains(clue.clueName);

            case Clue.ClueType.Attributo:
                return attributes.Contains(clue.clueName);

            case Clue.ClueType.Metodo:
                return methods.Contains(clue.clueName);

                
            case Clue.ClueType.Classe:
                ClassValue c = classi.Find(x => x.className == clue.clueName);
                return c!=null;
                

                /*
            case Clue.ClueType.Oggetto:
                return oggetti.Contains(clue.clueName);
                */
        }

        return true;
    }

    public void PickUpClue(Clue clue)
    {
        switch (clue.clueType)
        {
            case Clue.ClueType.Teoria:
                teoria.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;

            case Clue.ClueType.Attributo:
                attributes.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName,clue.clueType);
                break;

            case Clue.ClueType.Metodo:
                methods.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;

                
            case Clue.ClueType.Classe:
                classi.Add(clue.gameObject.GetComponent<ClasseEscape>().classValue);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;
                

                /*
            case Clue.ClueType.Oggetto:
                oggetti.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;
                */
        }
    }

    public void PickUpObject(OggettoEscape oggetto)
    {
        oggetti.Add(oggetto);
        inventoryLoad.AddItem(oggetto.objectName, Clue.ClueType.Oggetto);
    }
}
