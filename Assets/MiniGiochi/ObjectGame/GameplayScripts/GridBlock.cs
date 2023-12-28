using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBlock : MonoBehaviour
{
    private  Image image;
    private Color imageColor;
    [SerializeField]  Color temporaryColor ;
    [SerializeField]  Color temporaryConnectionColor;

    public bool isSelected = false;
    public GameManager_ObjectGame gameManager;

    public bool isConnected=false;
    [SerializeField] Color connectColor;

    public bool isStartingBlock;
    public bool isEndingBlock;

    public InventorySelection inventoryReference;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
        gameManager = FindObjectOfType<GameManager_ObjectGame>();
        if(isStartingBlock) { isConnected = true; }
    }

    public void SelectBlock()
    {
        isSelected = true;
    }

    public void DeselectBlock()
    {
        if (!isConnected)
        {
            isSelected = false;
            image.color = imageColor;
        }
        else
        {
            isSelected = false;
            image.color = connectColor;
        }
    }

    public virtual void RotateBlock(bool isLeftRotation)
    {
        if (!isLeftRotation) { transform.Rotate(0, 0, -90); }
        else { transform.Rotate(0, 0, 90); }
    }

    private void OnMouseDown()
    {
        if (!gameManager.isTemporaryItemDragging && !isStartingBlock && !isEndingBlock && !isSelected && !gameManager.isABlockSelected)
        {
            if (!isConnected)
                image.color = temporaryColor;
            else
                image.color = temporaryConnectionColor;

            gameManager.SelectBlock(this);
        }
    }

    private void OnMouseEnter()
    {
        if (!gameManager.isTemporaryItemDragging && !isStartingBlock &&  !isEndingBlock && !isSelected && !gameManager.isABlockSelected) { if (isConnected) { image.color = temporaryConnectionColor; } else { image.color = temporaryColor; } }
    }

    private void OnMouseExit()
    {
        if (!gameManager.isTemporaryItemDragging && !isStartingBlock && !isEndingBlock &&  !isSelected && !gameManager.isABlockSelected) { if (isConnected) { image.color = connectColor; } else { image.color = imageColor; } }
    }

    public virtual void ConnectBlock()
    {
        isConnected = true;
        if (isSelected) image.color = temporaryConnectionColor;
        else image.color = connectColor;

        if(isEndingBlock) { gameManager.VerifyAttributeValue(); }
    }

    public virtual void DisconnectBlock()
    {
        isConnected = false;
        if (isSelected) { image.color = temporaryColor; }
        else { image.color = imageColor; }
    }
}
