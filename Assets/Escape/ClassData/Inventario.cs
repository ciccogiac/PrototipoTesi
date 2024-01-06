using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    //Rappresenta l'inventario temporaneo in una scena di escape . I dati verranno poi passati ai datipersitenti per una scena di minigioco(Nella classe ClassGameStarter) o di escape.
    public List<string> teoria;
    public List<string> methods;
    public List<string> attributes;
    public List<string> classi;
    public List<string> oggetti;

    [SerializeField] InventoryLoad inventoryLoad;


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
                classi.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;

            case Clue.ClueType.Oggetto:
                oggetti.Add(clue.clueName);
                inventoryLoad.AddItem(clue.clueName, clue.clueType);
                break;
        }
    }
}
