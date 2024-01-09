using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadEscapeData : MonoBehaviour
{
    private GameObject player;
    private InventoryLoad inventoryLoad;
    // Start is called before the first frame update
    private void Start()
    {
        //Get player e setta la posizione all'ultima avuto in modalità di escape. Bisogna disabilitare temporaneamente il charactercontroller altrimenti non fuzniona
        player=GameObject.FindWithTag("Player");
        inventoryLoad = FindObjectOfType<InventoryLoad>();

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position =DatiPersistenti.istanza.lastCharacterEscapePosition;
        player.GetComponent<CharacterController>().enabled = true;

        Inventario.istanza.inventoryLoad = inventoryLoad;
    }

    

}
