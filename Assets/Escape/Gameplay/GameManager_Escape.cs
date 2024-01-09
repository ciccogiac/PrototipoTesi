using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Escape : MonoBehaviour
{
    public Clue[] clues;
    // Start is called before the first frame update
    void Start()
    {
        clues = FindObjectsOfType<Clue>();

        foreach(var clue in clues)
        {
            if (Inventario.istanza.IsCluePickedUp(clue) || Inventario.istanza.IsClueUsed(clue) ) { Destroy(clue.gameObject); }
        }
    }

}
