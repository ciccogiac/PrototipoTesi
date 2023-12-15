using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadEscapeData : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        //Get player e setta la posizione all'ultima avuto in modalità di escape. Bisogna disabilitare temporaneamente il charactercontroller altrimenti non fuzniona
        player=GameObject.FindWithTag("Player");
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position =DatiPersistenti.istanza.lastCharacterEscapePosition;
        player.GetComponent<CharacterController>().enabled = true;
    }

    

}
