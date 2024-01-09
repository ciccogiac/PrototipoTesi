using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : Interactable
{

    public enum ClueType
    {
        Teoria,
        Attributo,
        Metodo,
        Classe,
        Oggetto
    }
    public ClueType clueType;

    public string clueName;

    override public void Interact()
    {
        gameObject.SetActive(false);
        Inventario.istanza.PickUpClue(this);  
        Destroy(this);
    }
 }
