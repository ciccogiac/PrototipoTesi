using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TMPro;

public class ReadObject : Interactable
{

    private GameManager_Escape gameManager;

    [SerializeField] string text;

    private bool isReading = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
    }
    public override void Interact()
    {
        gameManager.ActivateReadObjectCanvas(text);
        isReading = true;
    }

    private void Update()
    {
        if (isReading && gameManager._input.readObject == true)
        { 
            CloseReadCanvas();
        }
    }

    public void CloseReadCanvas()
    {
        
        gameManager.DeactivateReadObjectCanvas();
        isReading = false;
        gameManager._input.readObject = false;
    }
}
