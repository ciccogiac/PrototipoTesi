using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private SaveManager saveManager;

    [SerializeField] GameObject button_NewGame;
    [SerializeField] GameObject button_ContinueGame;

    [SerializeField] GameObject Vbox;
    [SerializeField] GameObject Vbox_NewGame;

    private int level;

    private void Start()
    {
        if (PlayMusic.istanza != null) { Destroy(PlayMusic.istanza.gameObject); }

        saveManager = FindObjectOfType<SaveManager>();

        cursorHotspot = new Vector2(0, 0);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        LoadSave();
    }

    public void LoadSave()
    {
        level = saveManager.LoadSave().Item1;
        Inventario.istanza.teoria = saveManager.LoadSave().Item2;

        if (level == 0 || level == 1)
        {
            button_ContinueGame.SetActive(false);

        }
    }

    public void ActivateNewGameBox()
    {
        Vbox.SetActive(false);
        Vbox_NewGame.SetActive(true);
    }

    public void DeactivateNewGameBox()
    {
        Vbox_NewGame.SetActive(false);
        Vbox.SetActive(true);
        
    }
    public void NewGame()
    {
        level = 1;
        Inventario.istanza.teoria = new List<(string, string)>();
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void ExitGame()
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
