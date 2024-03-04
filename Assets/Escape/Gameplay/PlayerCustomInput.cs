using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCustomInput : MonoBehaviour
{
    public StarterAssetsInputs _input;
    [SerializeField] float rayLength;

    private Interactable last_InteractableObject = null;

    [SerializeField] GameObject canvasInventory;
    private bool inventoryState = false;

    public GameObject CanvasInteract;
    [SerializeField] TextMeshProUGUI mouseText;

    private GameManager_Escape gameManager;

    [SerializeField] GameObject canvasExit;
    private bool exitState = false;

    public bool _stopRaycast = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
    }

    private void checkInventoryInput()
    {
        if (_input.inventory == true)
        {
            _input.inventory = false;
            inventoryState = !inventoryState;
           


            Cursor.visible = inventoryState;
            if(inventoryState)
                Cursor.lockState = CursorLockMode.Confined;
            else
                Cursor.lockState = CursorLockMode.Locked;

            canvasInventory.SetActive(inventoryState);
        }
    }

    private void checkExitInput()
    {
        if (_input.exit == true)
        {
            _input.exit = false;
            exitState = !exitState;



            Cursor.visible = exitState;
            if (exitState)
                Cursor.lockState = CursorLockMode.Confined;
            else
                Cursor.lockState = CursorLockMode.Locked;

            canvasExit.SetActive(exitState);
        }
    }


    private void setMouseText(GameObject oggettoColpito)
    {
            ObjectInteraction o = oggettoColpito.GetComponent<ObjectInteraction>();
            if (o != null) { mouseText.text = "Interact"; return; }

            Clue c = oggettoColpito.GetComponent<Clue>();
            if (c != null && c.isActive) { mouseText.text = "Pick Up"; return; }

           

            ClassGameStarter cs = oggettoColpito.GetComponent<ClassGameStarter>();
            if (cs != null) { mouseText.text = "Class Creator"; return; }


            Printer3DController os = oggettoColpito.GetComponent<Printer3DController>();
            if (os != null) { mouseText.text = "3D Printer"; return; }

            ReadObject ro = oggettoColpito.GetComponent<ReadObject>();
            if (ro != null) { mouseText.text = "Read"; return; }

            SwitchCameraObject sco = oggettoColpito.GetComponent<SwitchCameraObject>();
            if (sco != null) { mouseText.text = "Interact"; return; }
        


    }

    private void setMouseSwitchCameraText(GameObject oggettoColpito)
    {
        ObjectInteraction o = oggettoColpito.GetComponent<ObjectInteraction>();
        if (o != null) { Cursor.SetCursor(gameManager.cursorSwitchCameraInteractTexture, new Vector2(gameManager.cursorSwitchCameraInteractTexture.width / 2, gameManager.cursorSwitchCameraInteractTexture.height / 2), CursorMode.ForceSoftware); return; }

        OggettoEscape oe = oggettoColpito.GetComponent<OggettoEscape>();
        if (oe != null && oe.isActive) { Cursor.SetCursor(gameManager.cursorSwitchCameraPickUpTexture, new Vector2(gameManager.cursorSwitchCameraPickUpTexture.width / 2, gameManager.cursorSwitchCameraPickUpTexture.height / 2), CursorMode.ForceSoftware); return; }

        Clue c = oggettoColpito.GetComponent<Clue>();
        if (c != null && c.isActive) { Cursor.SetCursor(gameManager.cursorSwitchCameraPickUpTexture, new Vector2(gameManager.cursorSwitchCameraPickUpTexture.width / 2, gameManager.cursorSwitchCameraPickUpTexture.height / 2), CursorMode.ForceSoftware); return; }

        ReadObject ro = oggettoColpito.GetComponent<ReadObject>();
        if (ro != null) { Cursor.SetCursor(gameManager.cursorSwitchCameraReadTexture, new Vector2(gameManager.cursorSwitchCameraReadTexture.width / 2, gameManager.cursorSwitchCameraReadTexture.height / 2), CursorMode.ForceSoftware); return; }



    }

    private void PlayerRaycast()
    {
        if (!_stopRaycast)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Controlla se il raggio colpisce un oggetto
            if (Physics.Raycast(ray, out hit, rayLength))
            {

                // Ottieni il collider colpito
                Collider colliderColpito = hit.collider;

                // Esegui le azioni desiderate, ad esempio, accedi all'oggetto colpito
                if (colliderColpito != null)
                {
                    GameObject oggettoColpito = colliderColpito.gameObject;
                    //Debug.Log("Oggetto colpito: " + oggettoColpito.name);
                    if (oggettoColpito.CompareTag("Interactable"))
                    {
                        if (last_InteractableObject == null)
                        {
                            last_InteractableObject = oggettoColpito.GetComponent<Interactable>();
                            last_InteractableObject.RaycastEnter();
                            if (last_InteractableObject.isActive)
                            {
                                if (!gameManager.isSeeing)
                                {
                                    setMouseText(oggettoColpito);
                                    CanvasInteract.SetActive(true);
                                }
                                else
                                {
                                    setMouseSwitchCameraText(oggettoColpito);
                                }
                            }
                        }
                        //else if (last_InteractableObject != oggettoColpito.GetComponent<Interactable>())
                        else
                        {
                            Interactable i = oggettoColpito.GetComponent<Interactable>();

                            if (last_InteractableObject != i)
                            {
                                last_InteractableObject.RaycastExit();

                                if (!gameManager.isSeeing)
                                    CanvasInteract.SetActive(false);
                                else
                                    Cursor.SetCursor(gameManager.cursorSwitchCameraTexture, new Vector2(gameManager.cursorSwitchCameraTexture.width / 2, gameManager.cursorSwitchCameraTexture.height / 2), CursorMode.Auto);
                            }

                            last_InteractableObject = i;
                            last_InteractableObject.RaycastEnter();
                            if (last_InteractableObject.isActive)
                            {
                                if (!gameManager.isSeeing)
                                {
                                    setMouseText(oggettoColpito);
                                    CanvasInteract.SetActive(true);
                                }
                                else
                                {
                                    setMouseSwitchCameraText(oggettoColpito);
                                }
                            }
                        }

                        if (_input.interact == true)
                        {
                            _input.interact = false;
                            last_InteractableObject.Interact();
                        }

                    }
                    else if (last_InteractableObject != null)
                    {
                        last_InteractableObject.RaycastExit();

                        if (!gameManager.isSeeing)
                            CanvasInteract.SetActive(false);
                        else
                            Cursor.SetCursor(gameManager.cursorSwitchCameraTexture, new Vector2(gameManager.cursorSwitchCameraTexture.width / 2, gameManager.cursorSwitchCameraTexture.height / 2), CursorMode.Auto);

                        last_InteractableObject = null;
                    }
                }

            }

            else if (last_InteractableObject != null)
            {
                last_InteractableObject.RaycastExit();

                if (!gameManager.isSeeing)
                    CanvasInteract.SetActive(false);
                else
                    Cursor.SetCursor(gameManager.cursorSwitchCameraTexture, new Vector2(gameManager.cursorSwitchCameraTexture.width / 2, gameManager.cursorSwitchCameraTexture.height / 2), CursorMode.Auto);

                last_InteractableObject = null;
            }

            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        }

    }
    // Update is called once per frame
    void Update()
    {
        checkExitInput();
        if (!exitState)
        {
            checkInventoryInput();
            if (!inventoryState)
                PlayerRaycast();
        }
       
    }
}
