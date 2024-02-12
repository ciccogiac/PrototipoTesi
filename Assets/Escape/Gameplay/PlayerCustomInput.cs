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

    private void setMouseText(GameObject oggettoColpito)
    {

            Clue c = oggettoColpito.GetComponent<Clue>();
            if (c != null) { mouseText.text = "Pick Up"; return; }

            ObjectInteraction o = oggettoColpito.GetComponent<ObjectInteraction>();
            if (o != null) { mouseText.text = "Interact"; return; }

            ClassGameStarter cs = oggettoColpito.GetComponent<ClassGameStarter>();
            if (cs != null) { mouseText.text = "Open ClassGame"; return; }

            ObjectGameStarter os = oggettoColpito.GetComponent<ObjectGameStarter>();
            if (os != null) { mouseText.text = "Open ObjectGame"; return; }

            ReadObject ro = oggettoColpito.GetComponent<ReadObject>();
            if (ro != null) { mouseText.text = "Read"; return; }

            SwitchCameraObject sco = oggettoColpito.GetComponent<SwitchCameraObject>();
            if (sco != null) { mouseText.text = "See"; return; }
        


    }

    private void setMouseSwitchCameraText(GameObject oggettoColpito)
    {

        Clue c = oggettoColpito.GetComponent<Clue>();
        if (c != null) { Cursor.SetCursor(gameManager.cursorSwitchCameraPickUpTexture, new Vector2(gameManager.cursorSwitchCameraPickUpTexture.width / 2, gameManager.cursorSwitchCameraPickUpTexture.height / 2), CursorMode.ForceSoftware); return; }

        ReadObject ro = oggettoColpito.GetComponent<ReadObject>();
        if (ro != null) { Cursor.SetCursor(gameManager.cursorSwitchCameraReadTexture, new Vector2(gameManager.cursorSwitchCameraReadTexture.width / 2, gameManager.cursorSwitchCameraReadTexture.height / 2), CursorMode.ForceSoftware); return; }



    }

    private void PlayerRaycast()
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

                            if(!gameManager.isSeeing)
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
    // Update is called once per frame
    void Update()
    {

        checkInventoryInput();
        if(!inventoryState)
            PlayerRaycast();
       
    }
}
