using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCustomInput : MonoBehaviour
{
    public StarterAssetsInputs _input;
    [SerializeField] float rayLength;

    private Interactable last_InteractableObject = null;

    [SerializeField] GameObject canvasInventory;
    private bool inventoryState = false;


    private void checkInventoryInput()
    {
        if (_input.inventory == true)
        {
            _input.inventory = false;
            inventoryState = !inventoryState;
            canvasInventory.SetActive(inventoryState);


            Cursor.visible = inventoryState;
            if(inventoryState)
                Cursor.lockState = CursorLockMode.Confined;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
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
                    }
                    //else if (last_InteractableObject != oggettoColpito.GetComponent<Interactable>())
                    else
                    {
                        Interactable i = oggettoColpito.GetComponent<Interactable>();

                        if (last_InteractableObject != i) 
                            last_InteractableObject.RaycastExit();
                        
                        last_InteractableObject = i;
                        last_InteractableObject.RaycastEnter();
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
                    last_InteractableObject = null;
                }
            }

        }

        else if (last_InteractableObject != null)
        {
            last_InteractableObject.RaycastExit();
            last_InteractableObject = null;
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

    }
    // Update is called once per frame
    void Update()
    {

        checkInventoryInput();
        PlayerRaycast();
       
    }
}
