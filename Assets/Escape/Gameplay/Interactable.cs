using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool isActive = true;

    [SerializeField] Color outlineColor = Color.magenta;

    public void RaycastEnter()
    {
        if (isActive)
        {
            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Outline outline = gameObject.AddComponent<Outline>();
                outline.enabled = true;
                gameObject.GetComponent<Outline>().OutlineColor = outlineColor;
                gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
            }
        }
    }

    public void RaycastExit()
    {
        if ( isActive) 
        { 

            gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    public virtual void Interact() { }
}
