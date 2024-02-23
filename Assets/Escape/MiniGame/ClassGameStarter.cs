using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas_ClassGameInterface;

    private ClassDictionary dizionario;
    private GameManager_Escape gameManager;

    private void Start()
    {
       
        if (DatiPersistenti.istanza.isNewClassCreated)
        {
            DatiPersistenti.istanza.isNewClassCreated = false;

            dizionario = FindObjectOfType<ClassDictionary>();
            gameManager = FindObjectOfType<GameManager_Escape>();


            gameManager.ActivateNewItemCanvas(Clue.ClueType.Classe.ToString(), DatiPersistenti.istanza.className, dizionario.GetClassDescription(DatiPersistenti.istanza.className));

        }
    }
    override public void Interact()
    {
                DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;
                DatiPersistenti.istanza.lastCharacterEscapeRotation = player.transform.rotation;

                canvas_ClassGameInterface.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
    }
    
}
