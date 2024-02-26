using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VisibilityTutorial : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    [SerializeField] PlayerInput input;

    [SerializeField] GameObject[] items;
    int i = 0;

    [SerializeField] GameObject interactCanvas;
    [SerializeField] Button backButton;
    [SerializeField] Button okButton;

    [SerializeField] Door_level4 door;

   

    private void OnEnable()
    {
        input.enabled = false;


        cursorHotspot = new Vector2(0f, 0f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        i = 0;
        items[i].SetActive(true);
    }

    public void PreviousItem()
    {
        backButton.interactable = false;
        i--;
        if (i >= 0)
        {
            backButton.gameObject.SetActive(true);

            items[i].SetActive(true);
            items[i+1].SetActive(false);
            
            if (i==0)
                backButton.gameObject.SetActive(false);

        }
        backButton.interactable = true;

    }
    public void NextItem()
    {
        okButton.interactable = false;
        i++;
        if (i < items.Length )
        {
            backButton.gameObject.SetActive(true);

            if(i>0)
                items[i - 1].SetActive(false);

            items[i].SetActive(true);
            okButton.interactable = true;

        }
        else
        {
            gameObject.SetActive(false);  
            door.GetTeory();
        }
    }
}
