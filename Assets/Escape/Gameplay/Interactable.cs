using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject text_active;

    public void RaycastEnter()
    {
        text_active.SetActive(true);
    }

    public void RaycastExit()
    {
        text_active.SetActive(false);
    }

    public virtual void Interact() { }
}
