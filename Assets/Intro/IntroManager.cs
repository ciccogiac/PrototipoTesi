using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private SaveManager saveManager;

    [SerializeField] private bool GameEnded;

    [SerializeField] GameObject button_NewGame;
    [SerializeField] GameObject button_ContinueGame;

    [SerializeField] GameObject Vbox;
    [SerializeField] GameObject Vbox_NewGame;

    [SerializeField] private TMP_InputField ID_InputField;
    [SerializeField] private GameObject ConfirmID;
    [SerializeField] private GameObject IDPrompt;

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
        if (!GameEnded)
        {
            ID_InputField.Select();
            ID_InputField.ActivateInputField();
        }
        else
        {
            StartCoroutine(ServerUploader.ServerUploader.UploadToServer());
        }
    }

    public void ID_InputValueChanged()
    {
        var value = ID_InputField.text;
        if (value != null && value.Trim() != "" && !ConfirmID.activeSelf)
            ConfirmID.SetActive(true);
        if (ConfirmID.activeSelf && (value == null || value.Trim() == ""))
            ConfirmID.SetActive(false);
    }

    public void ID_Submit()
    {
        IDPrompt.SetActive(false);
        DatiPersistenti.IDCurrentSessionEsperimento = ID_InputField.text.Trim();
        DatiPersistenti.InitLog();
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
        // Questa parte verr� eseguita solo nell'Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Questa parte verr� eseguita durante l'esecuzione standalone
        Application.Quit();
#endif
    }
}
