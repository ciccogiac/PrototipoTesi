using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : Interactable
{
    [SerializeField] GameObject objectCollectedCanvas;
    public Transform objectPoint;

    public Clue oggetto;

    override public void Interact()
    {
        if (isActive)
        {
            if (oggetto == null)
            {
                Debug.Log("Interact with objectInteractor");
                objectCollectedCanvas.GetComponent<ObjectCollected_Canvas>().objectInteraction = this;
                objectCollectedCanvas.SetActive(true);
                text_active.SetActive(false);
            }

            else
            {
                Debug.Log("Interagisco con oggetto");
            }
        }
    }

    }
