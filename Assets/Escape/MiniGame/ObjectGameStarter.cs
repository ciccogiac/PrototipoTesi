using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGameStarter : Interactable
{
    [SerializeField] GameObject canvas_ObjectGameInterface;

    public GameObject oggettoDaIstanzare;
    public Transform printingPosition;

    private void Start()
    {
        if (DatiPersistenti.istanza.isObjectToPrint)
        {
            DatiPersistenti.istanza.isObjectToPrint=false;
            PrintObject();
        }
    }

    private void PrintObject()
    {
        string objectName = DatiPersistenti.istanza.objectName;
        GameObject nuovoOggetto = Instantiate(oggettoDaIstanzare, printingPosition.position, transform.rotation);
        Clue clue = nuovoOggetto.GetComponent<Clue>();
        clue.clueName = objectName;
        clue.clueType = Clue.ClueType.Oggetto;
    }
    override public void Interact()
    {
        canvas_ObjectGameInterface.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
}
