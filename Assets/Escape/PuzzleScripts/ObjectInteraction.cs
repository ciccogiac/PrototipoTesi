using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ObjectInstantiatorValues
{
    [SerializeField] public string ClassName;
    [SerializeField] public Quaternion Rotation;
    [SerializeField] public float Scale;
}

public class ObjectInteraction : Interactable
{
    [SerializeField] GameObject objectCollectedCanvas;
    [SerializeField] GameObject objectCallMethodsCanvas;
    public Transform objectPoint;
    [SerializeField] public List<ObjectInstantiatorValues> ObjectInstantiatorValuesList;

    public OggettoEscape oggetto;
    public MethodListener methodListener;

    public int Id=1;

    public bool isObjectPermanent = false;
    public bool isObjectSee = false;

    override public void Interact()
    {
        if (isActive)
        {
            if (oggetto == null && !isObjectPermanent)
            {
                objectCollectedCanvas.GetComponent<ObjectCollected_Canvas>().objectInteraction = this;
                objectCollectedCanvas.SetActive(true);
                //text_active.SetActive(false);
            }

            else
            {
                objectCallMethodsCanvas.GetComponent<ObjectCallMethods>().objectInteraction = this;
                objectCallMethodsCanvas.SetActive(true);
                //text_active.SetActive(false);
            }
        }
    }

    }
