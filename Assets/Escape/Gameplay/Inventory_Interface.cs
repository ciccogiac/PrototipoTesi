using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory_Interface : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] PlayerInput input;

    [SerializeField] GameObject interactionCanvas;
    [SerializeField] GameObject ItemsVisualizerVerticalBox;
    [SerializeField] GameObject ButtonVerticalBox;
    [SerializeField] Button[] buttons;
    public Color normalColor = Color.white;

    [SerializeField] GameObject DescriptionPanel;

    [SerializeField] GameManager_Escape gameManager;
    [SerializeField] GameObject interactionSwitchCameraCanvas;



    private void OnEnable()
    {
        
        ItemsVisualizerVerticalBox.SetActive(false);
        DescriptionPanel.SetActive(false);


        foreach (Button button in buttons)
        {
            button.interactable = false; // Rendi il bottone selezionabile
            button.interactable = true; // Rendi il bottone selezionabile
            ColorBlock colors = button.colors;
            colors.normalColor = normalColor;
            button.colors = colors;
        }


        //input.enabled = false;
        input.SwitchCurrentActionMap ( "Inventory");

        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        if (gameManager.isSeeing)
            interactionSwitchCameraCanvas.SetActive(false);
        else
            interactionCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        //input.SwitchCurrentActionMap("Player");
        //interactionCanvas.SetActive(true);

        if (gameManager.isSeeing)
        {
            input.SwitchCurrentActionMap("SwitchCamera");
            interactionSwitchCameraCanvas.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            input.SwitchCurrentActionMap("Player");
            interactionCanvas.SetActive(true);
        }
    }
}
