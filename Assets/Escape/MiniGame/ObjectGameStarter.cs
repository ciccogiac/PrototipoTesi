using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGameStarter : Interactable
{
    [SerializeField] GameObject canvas_ObjectGameInterface;

    override public void Interact()
    {
        canvas_ObjectGameInterface.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }
}
