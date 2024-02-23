using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager_Escape : MonoBehaviour
{
    public Clue[] clues;

    public ObjectInteraction[] objectInteractors;

    [SerializeField] GameObject objectPrefab;

    [SerializeField] GameObject readObjectCanvas;
    [SerializeField] GameObject interactionCanvas;
    public GameObject interactionSwitchCameraCanvas;
    [SerializeField] GameObject NewItemCanvas;
    [SerializeField] GameObject DialogCanvas;
    [SerializeField] private LongSpeech IntroSpeechCanvas;

    [SerializeField] TextMeshProUGUI ReadObjectText;

    public PlayerInput input;
    public StarterAssetsInputs _input;

    [SerializeField] NewItem itemCanvasScript;

    [SerializeField] CinemachineVirtualCamera primaryCamera;

    public bool isSeeing = false;

    public Texture2D cursorSwitchCameraTexture;
    private Vector2 cursorSwitchCameraHotspot;

    public Texture2D cursorSwitchCameraPickUpTexture;
    public Texture2D cursorSwitchCameraReadTexture;
    public Texture2D cursorSwitchCameraInteractTexture;

    [SerializeField] CinemachineBrain cinemachineBrain;

    public Printer3DController printer;

    private SaveManager saveManager;

    public CinemachineVirtualCamera DialogCamera = null;

    private LevelHint _levelHint;

#if UNITY_EDITOR
    // Questo metodo viene chiamato solo nell'editor quando si fa clic su "Gestisci valori" nel componente
    [ContextMenu("Calcola valori id iniziali")]
     void ImpostaValoriID()
    {
        Debug.Log("Calcolo id");
        int i = 0;
        foreach (var x in FindObjectsOfType<ObjectInteraction>())
        {
            x.Id = i;
            i++;
        }

        i = 0;
        foreach (var x in FindObjectsOfType<MethodListener>())
        {
            x.methodListenerID = i;
            i++;
        }

        i = 0;
        foreach (var x in FindObjectsOfType<DialogStarter>())
        {
            x._dialogID = i;
            i++;
        }

    }
#endif


    // Start is called before the first frame update
    void Start()
    {


        interactionCanvas.SetActive(true);
        interactionSwitchCameraCanvas.SetActive(false);
        readObjectCanvas.SetActive(false);     
        NewItemCanvas.SetActive(false);

        saveManager = FindObjectOfType<SaveManager>();
        printer =FindObjectOfType<Printer3DController>();
        _levelHint = FindObjectOfType<LevelHint>();

        Cursor.lockState = CursorLockMode.Locked;

        if (DatiPersistenti.istanza.isFirstSceneOpening)
        {
            DatiPersistenti.istanza.isFirstSceneOpening = false;
            saveManager.Save(SceneManager.GetActiveScene().buildIndex, Inventario.istanza.teoria);
            ActivateLongSpeechCanvas(IntroSpeechCanvas.gameObject);

            StarterObjectsInitialize s = GetComponent<StarterObjectsInitialize>();
            if (s != null)
                s.Initializeobjects();
        }



        ReloadObjectScene();
        
    }

    private void ReloadObjectScene()
    {
        


       

        DialogStarter[] dialogs = FindObjectsOfType<DialogStarter>();

        foreach (var dialog in DatiPersistenti.istanza.dialogUsed)
        {

            foreach (var x in dialogs)
            {
                if (x._dialogID == dialog)
                {
                    x._dialogUsed = true;
                }
            }
        }


        MethodListener[] methodsListeners = FindObjectsOfType<MethodListener>();

        foreach (var methodListener in DatiPersistenti.istanza.methodsListeners)
        {

            foreach (var x in methodsListeners)
            {
                if (x.methodListenerID == methodListener)
                {
                    x.ApplyMethod();
                }
            }


        }

        clues = FindObjectsOfType<Clue>();

        foreach (var clue in clues)
        {
            if (Inventario.istanza.IsCluePickedUp(clue) || Inventario.istanza.IsClueUsed(clue)) { Destroy(clue.gameObject); }
        }

        objectInteractors = FindObjectsOfType<ObjectInteraction>();


        foreach (var objectInteractor in objectInteractors)
        {
            OggettoEscapeValue o = Inventario.istanza.oggettiUsed.Find(x => x.ObjectInteractorId == objectInteractor.Id);
            if (o != null)
            {
                InstanziaOggetto(o, objectInteractor);
            }
        }

    }

    private void InstanziaOggetto(OggettoEscapeValue oggettoEscapeValue , ObjectInteraction objectInteraction)
    {
        GameObject oggettoIstanziato = Instantiate(oggettoEscapeValue.classPrefab, objectInteraction.objectPoint.position, objectInteraction.Rotation);
        oggettoIstanziato.GetComponent<OggettoEscape>().SetOggettoEscapeValue(oggettoEscapeValue);
        oggettoIstanziato.GetComponent<OggettoEscape>().isActive = false;
        oggettoIstanziato.GetComponent<Collider>().enabled = false;

        oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
        oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

        //float fattoreScala = 4f;
        //oggettoIstanziato.gameObject.transform.localScale *= fattoreScala;
        oggettoIstanziato.transform.localScale = objectInteraction.Scale * Vector3.one;
        oggettoIstanziato.gameObject.SetActive(true);

        //oggettoIstanziato.GetComponent<OggettoEscape>().SetObjectValue();
        objectInteraction.oggetto = oggettoIstanziato.GetComponent<OggettoEscape>();
        objectInteraction.oggetto.methodListener = objectInteraction.methodListener;
        objectInteraction.oggetto.methodListener.SetClass(objectInteraction.oggetto.oggettoEscapeValue.className);


    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void SwitchCamera(CinemachineVirtualCamera objectCamera)
    {
        isSeeing = true;
        
        //input.SwitchCurrentActionMap("SwitchCamera");
        input.enabled = false;


        primaryCamera.enabled = false;
        objectCamera.enabled = true;

        interactionCanvas.SetActive(false);
        interactionSwitchCameraCanvas.SetActive(true);

        cursorSwitchCameraHotspot = new Vector2(cursorSwitchCameraTexture.width/2, cursorSwitchCameraTexture.height / 2);
        Cursor.SetCursor(cursorSwitchCameraTexture, cursorSwitchCameraHotspot, CursorMode.Auto);
        input.enabled = true;
        input.SwitchCurrentActionMap("SwitchCamera");
    }

    public void SwitchCameraToPrimary(CinemachineVirtualCamera objectCamera)
    {
        input.enabled = false;
        objectCamera.enabled = false;
        primaryCamera.enabled = true;


        if (cinemachineBrain.IsBlending) 
            StartCoroutine(waitSwitchCamera(cinemachineBrain.ActiveBlend.TimeInBlend));
        else
            StartCoroutine(waitSwitchCamera(cinemachineBrain.m_DefaultBlend.m_Time));

        

    }

    IEnumerator waitSwitchCamera(float duration)
    {
        yield return new WaitForSeconds(duration);
        input.enabled = true;
        input.SwitchCurrentActionMap("Player");

        interactionSwitchCameraCanvas.SetActive(false);
        interactionCanvas.SetActive(true);
        isSeeing = false;
    }

    public void ActivateReadObjectCanvas(string text)
    {
        input.enabled = true;
        input.SwitchCurrentActionMap("ReadObject");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ReadObjectText.text = text;

        if (isSeeing)
            interactionSwitchCameraCanvas.SetActive(false);
        else
            interactionCanvas.SetActive(false);

        readObjectCanvas.SetActive(true);

    }

    public void DeactivateReadObjectCanvas()
    {

        if (isSeeing)
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

        readObjectCanvas.SetActive(false);
    }

    public void ActivateNewItemCanvas(string type, string name, string description)
    {
        input.enabled = true;
        input.SwitchCurrentActionMap("ReadObject");
        itemCanvasScript.InitializeItemCanvas(type, name, description);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (isSeeing) 
            interactionSwitchCameraCanvas.SetActive(false);
        else
            interactionCanvas.SetActive(false);

        NewItemCanvas.SetActive(true);
    }

    public void DeactivateNewItemCanvas()
    {
        

        NewItemCanvas.SetActive(false);

        if (DialogCamera == null)
        {
            if (isSeeing)
            {
                input.SwitchCurrentActionMap("SwitchCamera");
                interactionSwitchCameraCanvas.SetActive(true);

                Cursor.SetCursor(cursorSwitchCameraTexture, new Vector2(cursorSwitchCameraTexture.width / 2, cursorSwitchCameraTexture.height / 2), CursorMode.Auto);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                input.SwitchCurrentActionMap("Player");
                interactionCanvas.SetActive(true);
            }
        }

        else
        {
            SwitchCameraToPrimary(DialogCamera);
            DialogCamera = null;
        }
            

    }

    public void ActivateDialogCanvas()
    {
        interactionCanvas.SetActive(false);
        DialogCanvas.SetActive(true);

        input.enabled = true;
        input.SwitchCurrentActionMap("Dialog");
    }

    public void DeactivateDialogCanvas()
    {
        interactionCanvas.SetActive(true);
        DialogCanvas.SetActive(false);

        //input.SwitchCurrentActionMap("Player");
    }
    
    private void ActivateLongSpeechCanvas(GameObject longSpeech)
    {
        interactionCanvas.SetActive(false);
        longSpeech.SetActive(true);

        input.enabled = true;
        input.SwitchCurrentActionMap("Dialog");
    }

    public void DeactivateLongSpeechCanvas(GameObject longSpeech)
    {
        interactionCanvas.SetActive(true);
        longSpeech.SetActive(false);
        input.SwitchCurrentActionMap("Player");

        if (_levelHint != null && _levelHint.hint.Length >0) 
            _levelHint.StartHintCounter();
    }
}

