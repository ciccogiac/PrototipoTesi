using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadEscapeData : MonoBehaviour
{
    private GameObject player;
    private InventoryLoad inventoryLoad;
    private LevelHint levelHint;
    // Start is called before the first frame update
    private void Start()
    {
        //Get player e setta la posizione all'ultima avuto in modalit� di escape. Bisogna disabilitare temporaneamente il charactercontroller altrimenti non fuzniona
        player = GameObject.FindWithTag("Player");
        inventoryLoad = FindObjectOfType<InventoryLoad>();

        player.GetComponent<CharacterController>().enabled = false;
        levelHint = FindObjectOfType<LevelHint>();

        if (!DatiPersistenti.istanza.isFirstSceneOpening)
        {
            player.transform.position = DatiPersistenti.istanza.lastCharacterEscapePosition;
            player.transform.rotation = DatiPersistenti.istanza.lastCharacterEscapeRotation;
            levelHint.hintCounter = DatiPersistenti.istanza.hintCounter;
            levelHint.StartHintCounter();
        }


        player.GetComponent<CharacterController>().enabled = true;

        Inventario.istanza.inventoryLoad = inventoryLoad;

        
       
    }

    

}
