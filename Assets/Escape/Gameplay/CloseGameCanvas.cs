using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseGameCanvas : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] PlayerInput input;

    [SerializeField] GameObject interactionCanvas;

    private void OnEnable()
    {
        interactionCanvas.SetActive(false);

        input.SwitchCurrentActionMap("Exit");

        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        input.SwitchCurrentActionMap("Player");
        interactionCanvas.SetActive(true);
    }
    public void CloseGame()
    {
#if UNITY_EDITOR
        // Questa parte verrà eseguita solo nell'Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Questa parte verrà eseguita durante l'esecuzione standalone
        Application.Quit();
#endif
    }
}
