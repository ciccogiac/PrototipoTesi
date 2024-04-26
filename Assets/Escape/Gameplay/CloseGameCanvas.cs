using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CloseGameCanvas : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] PlayerInput input;

    [SerializeField] GameObject interactionCanvas;

    [SerializeField] private GameObject CommandsPanel;
    [SerializeField] private GameObject PausePanel;

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
        // Questa parte verr� eseguita solo nell'Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Questa parte verr� eseguita durante l'esecuzione standalone
        Application.Quit();
#endif
    }

    public void ShowCommands()
    {
        PausePanel.SetActive(false);
        CommandsPanel.SetActive(true);
    }

    public void HideCommands()
    {
        CommandsPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Intro/Intro2", LoadSceneMode.Single);
    }
}
