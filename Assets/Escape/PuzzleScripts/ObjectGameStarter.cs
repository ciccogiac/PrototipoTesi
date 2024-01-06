using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGameStarter : Interactable
{
    [SerializeField] GameObject player;

    override public void Interact()
    {

        DatiPersistenti.istanza.lastCharacterEscapePosition = player.transform.position;
        SceneManager.LoadScene("ObjectGame");


    }
}
