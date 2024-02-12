using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStarter : MonoBehaviour
{
    private GameManager_Escape gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ActivateDialog();
        }
    }
}
