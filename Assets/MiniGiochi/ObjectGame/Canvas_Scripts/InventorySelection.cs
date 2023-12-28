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
    [SerializeField] int itemCount = 0;

    [SerializeField] GameObject temporaryItem;

    public enum BlockDirection
    {
        Forward,
        Right,
        Left
    }

    public BlockDirection direction;

    private GameManager_ObjectGame gameManager;



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
        if (!gameManager.isTemporaryItemDragging && itemCount > 0)
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
        if (!gameManager.isTemporaryItemDragging)
        {
            image.color = temporaryColor;
        }
    }

    private void OnMouseExit()
    {
        if (!gameManager.isTemporaryItemDragging)
        {
            image.color = imageColor;
        }
    }
}
