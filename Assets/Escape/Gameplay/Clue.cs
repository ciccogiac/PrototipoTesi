using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : Interactable
{
    private Inventario inventario;

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


    // Start is called before the first frame update
    void Start()
    {
        inventario = FindObjectOfType<Inventario>();
    }

    override public void Interact()
    {
        gameObject.SetActive(false);
        inventario.PickUpClue(this);  
        Destroy(this);
    }
 }
