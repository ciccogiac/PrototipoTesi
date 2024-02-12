using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager_Escape : MonoBehaviour
{
    public Clue[] clues;

    public ObjectInteraction[] objectInteractors;

    [SerializeField] GameObject objectPrefab;

    [SerializeField] GameObject readObjectCanvas;
    [SerializeField] GameObject interactionCanvas;
    [SerializeField] GameObject interactionSwitchCameraCanvas;
    [SerializeField] GameObject NewItemCanvas;

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

    [SerializeField] CinemachineBrain cinemachineBrain;

    // Start is called before the first frame update
    void Start()
    {
        interactionCanvas.SetActive(true);
        interactionSwitchCameraCanvas.SetActive(false);
        readObjectCanvas.SetActive(false);     
        NewItemCanvas.SetActive(false);



        clues = FindObjectsOfType<Clue>();

        foreach(var clue in clues)
        {
            if (Inventario.istanza.IsCluePickedUp(clue) || Inventario.istanza.IsClueUsed(clue) ) { Destroy(clue.gameObject); }
        }

        objectInteractors = FindObjectsOfType<ObjectInteraction>();

        foreach (var objectInteractor in objectInteractors)
        {
            OggettoEscapeValue o = Inventario.istanza.oggettiUsed.Find(x => x.ObjectInteractorId !=0 && x.ObjectInteractorId == objectInteractor.Id);
            if (o != null)
            {
                InstanziaOggetto(o, objectInteractor);
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
    }


    private void InstanziaOggetto(OggettoEscapeValue oggettoEscapeValue , ObjectInteraction objectInteraction)
    {
        GameObject oggettoIstanziato = Instantiate(objectPrefab, objectInteraction.objectPoint.position, Quaternion.identity);
        oggettoIstanziato.GetComponent<OggettoEscape>().SetOggettoEscapeValue(oggettoEscapeValue);

        oggettoIstanziato.transform.position = objectInteraction.objectPoint.position;
        oggettoIstanziato.transform.SetParent(objectInteraction.gameObject.transform);

        oggettoIstanziato.gameObject.GetComponent<MeshFilter>().mesh = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.mesh;
        oggettoIstanziato.gameObject.GetComponent<MeshRenderer>().materials = oggettoIstanziato.GetComponent<OggettoEscape>().oggettoEscapeValue.material;
        float fattoreScala = 0.5f;
        oggettoIstanziato.gameObject.transform.localScale *= fattoreScala;
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
        input.SwitchCurrentActionMap("SwitchCamera");

        primaryCamera.enabled = false;
        objectCamera.enabled = true;

        interactionCanvas.SetActive(false);
        interactionSwitchCameraCanvas.SetActive(true);

        cursorSwitchCameraHotspot = new Vector2(cursorSwitchCameraTexture.width/2, cursorSwitchCameraTexture.height / 2);
        Cursor.SetCursor(cursorSwitchCameraTexture, cursorSwitchCameraHotspot, CursorMode.Auto);
    }

    public void SwitchCameraToPrimary(CinemachineVirtualCamera objectCamera)
    {
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
        input.SwitchCurrentActionMap("Player");

        interactionSwitchCameraCanvas.SetActive(false);
        interactionCanvas.SetActive(true);
        isSeeing = false;
    }

    public void ActivateReadObjectCanvas(string text)
    {
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
}
