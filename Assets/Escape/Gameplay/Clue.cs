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
    public string clueDescription;

    private PlayerCustomInput customInput;

    private GameManager_Escape gameManager;

    private void Start()
    {
        customInput = FindObjectOfType<PlayerCustomInput>();
        gameManager = FindObjectOfType<GameManager_Escape>();
    }

    override public void Interact()
    {
        customInput.CanvasInteract.SetActive(false);

        gameObject.SetActive(false);
        Inventario.istanza.PickUpClue(this);
        gameManager.ActivateNewItemCanvas(clueType.ToString(),clueName,clueDescription);
        Destroy(this);
    }
 }
