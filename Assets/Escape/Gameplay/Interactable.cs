using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject text_active;

    public bool isActive = true;

    public void RaycastEnter()
    {
        if (isActive){ text_active.SetActive(true); }
    }

    public void RaycastExit()
    {
        if (isActive) { text_active.SetActive(false); }
    }

    public virtual void Interact() { }
}
