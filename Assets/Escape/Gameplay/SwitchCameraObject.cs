using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class SwitchCameraObject : Interactable
{
    
    [SerializeField] CinemachineVirtualCamera objectCamera;

    private GameManager_Escape gameManager;
    private bool isSeeing = false;

    private List<OggettoEscape> oggetti;
    private List<ReadObject> oggettiRead;
    private List<Clue> clues;

    [SerializeField] GameObject padre;

    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
        oggetti = padre.GetComponentsInChildren<OggettoEscape>().ToList();
        oggettiRead = padre.GetComponentsInChildren<ReadObject>().ToList();
        //clues = padre.GetComponentsInChildren<Clue>().ToList();
        clues = padre.GetComponentsInChildren<Clue>().Where(clue => clue.GetComponent<OggettoEscape>() == null).ToList();

        ChangeObjectActivateState(false);
    }

    private void ChangeObjectActivateState(bool state)
    {
        foreach(var x in oggetti)
        {
            if (x != null)
            {
                x.isActive = state;
                if (!state) 
                {
                    Outline o = x.gameObject.GetComponent<Outline>();
                    if(o!=null) o.enabled = false;
                    x.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                else
                    x.gameObject.GetComponent<SphereCollider>().enabled = true;

            }
        }

        foreach (var x in oggettiRead)
        {
            if (x != null)
            {
                x.isActive = state;
                if (!state)
                {
                    Outline o = x.gameObject.GetComponent<Outline>();
                    if (o != null) o.enabled = false;
                    x.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                else
                    x.gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }

        foreach (var x in clues)
        {
            if (x != null)
            {
                x.isActive = state;
                if (!state)
                {
                    Outline o = x.gameObject.GetComponent<Outline>();
                    if (o != null) o.enabled = false;
                    x.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                else
                    x.gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    public override void Interact()
    {
        gameManager.SwitchCamera(objectCamera);

        isSeeing = true;
        isActive = false;

        Outline outline = gameObject.GetComponent<Outline>();
        if (outline!=null)
            outline.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        ChangeObjectActivateState(true);
        
        
    }

    private void Update()
    {
        if (isSeeing && gameManager._input.backCamera == true)
        {
            ReturnToPrimaryCamera();
        }
    }

    public void ReturnToPrimaryCamera()
    {

        gameManager.SwitchCameraToPrimary(objectCamera);
        isSeeing = false;
        gameManager._input.backCamera = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isActive = true;
        ChangeObjectActivateState(false);
    }
}
