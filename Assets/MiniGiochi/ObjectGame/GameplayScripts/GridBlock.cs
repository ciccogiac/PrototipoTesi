using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBlock : MonoBehaviour
{
    private  Image image;
    private Color imageColor;
    [SerializeField]  Color temporaryColor ;

    private bool isSelected = false;
    [SerializeField] GameManager_ObjectGame gameManager;

    public bool isConnected=false;
    [SerializeField] Color connectColor;
    private Color previousColor;
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
        image.color = imageColor;
    }

    public void RotateBlock(bool isLeftRotation)
    {
        if (!isLeftRotation) { transform.Rotate(0, 0, -90); }
        else { transform.Rotate(0, 0, 90); }
    }

    private void OnMouseDown()
    {
        image.color = temporaryColor;

        gameManager.SelectBlock(this);
    }

    private void OnMouseEnter()
    {
        if (!isSelected && !gameManager.isABlockSelected) { image.color = temporaryColor; }
    }

    private void OnMouseExit()
    {
        if (!isSelected && !gameManager.isABlockSelected) { image.color = imageColor; }
    }

    public void ConnectBlock()
    {
        previousColor = image.color;
        isConnected = true;
        image.color = connectColor;
    }

    public void DisconnectBlock()
    {
        isConnected = false;
        image.color = previousColor;
    }
}
