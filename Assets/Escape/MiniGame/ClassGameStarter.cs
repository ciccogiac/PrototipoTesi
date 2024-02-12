using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas_ClassGameInterface;
    
    override public void Interact()
    {
                DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;

                canvas_ClassGameInterface.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
    }
    
}
