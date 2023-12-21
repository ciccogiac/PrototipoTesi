using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBlock : MonoBehaviour
{
    private  Image image;
    private Color imageColor;
    [SerializeField]  Color temporaryColor ;

    public bool isSelected = false;
    [SerializeField] GameManager_ObjectGame gameManager;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
        gameManager = FindObjectOfType<GameManager_ObjectGame>();
    }

    public void SelectBlock()
    {
        isSelected = true;
    }

    public void DeselectBlock()
    {
        isSelected = false;
    }

    public void RotateBlock(bool isLeftRotation)
    {
        if (!isLeftRotation) { transform.Rotate(0, 0, 90); }
        else { transform.Rotate(0, 0, -90); }
    }

    private void OnMouseDown()
    {
        Debug.Log("premuto" + " z rotation = " + transform.rotation.z);
        image.color = temporaryColor;

        gameManager.SelectBlock(this);
    }

    private void OnMouseEnter()
    {
        if (!isSelected) { image.color = temporaryColor; }
    }

    private void OnMouseExit()
    {
        if (!isSelected) { image.color = imageColor; }
    }

    private void OnMouseUp()
    {
        Debug.Log("rilasciato");
        //image.color = imageColor;
    }
}
