using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelection : MonoBehaviour
{
    private Image image;
    private Color imageColor;
    [SerializeField] Color temporaryColor;

    [SerializeField] TextMeshProUGUI itemCount_text;
    public int itemCount = 0;

    [SerializeField] GameObject temporaryItem;

    public enum BlockDirection
    {
        Forward,
        Right,
        Left
    }

    public BlockDirection direction;

    public enum BlockType
    {
        Connection,
        Integer,
        Boolean,
        Char
    }

    public BlockType blockType;

    public int intValue = 0;
    public bool boolValue = false;
    public char charValue = ' ';
    [SerializeField] TextMeshProUGUI itemValue_text;

    private GameManager_ObjectGame gameManager;

    private void OnValidate()
    {
        UpdateItemValue();
    }

    public void UpdateItemValue()
    {
        itemCount_text.text = itemCount.ToString();

        switch (blockType)
        {
            case BlockType.Connection:
                break;

            case BlockType.Integer:
                if (intValue > 0) { itemValue_text.text = "+" + intValue.ToString(); }
                else { itemValue_text.text = intValue.ToString(); }
                break;

            case BlockType.Boolean:
                itemValue_text.text = boolValue.ToString();
                break;

            case BlockType.Char:
                itemValue_text.text = charValue.ToString();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager_ObjectGame>();
        image = GetComponent<Image>();
        imageColor = image.color;

        itemCount_text.text = itemCount.ToString();

       
    }

    public void RestoreImage() { image.color = imageColor; }

    public void CancelDeleteItem() { itemCount++; itemCount_text.text = itemCount.ToString();  gameManager.isTemporaryItemDragging = false; }


    private void OnMouseDown()
    {
        if (!gameManager.isTemporaryItemDragging && !gameManager.isABlockSelected && itemCount > 0)
        {
            itemCount --;
            itemCount_text.text = itemCount.ToString();

            gameManager.trash.SetActive(true);

            Cursor.visible = false;
            gameManager.isTemporaryItemDragging = true;

            GameObject g= Instantiate(temporaryItem);
            g.GetComponent<TemporaryBlock>().inventoryReference = this;
        }
    }

    private void OnMouseEnter()
    {
        if (!gameManager.isTemporaryItemDragging && !gameManager.isABlockSelected)
        {
            image.color = temporaryColor;
        }
    }

    private void OnMouseExit()
    {
        if (!gameManager.isTemporaryItemDragging && !gameManager.isABlockSelected)
        {
            image.color = imageColor;
        }
    }
}
