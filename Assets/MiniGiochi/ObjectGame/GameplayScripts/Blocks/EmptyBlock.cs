using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyBlock : MonoBehaviour
{
    private Image image;
    private Color imageColor;
    [SerializeField] Color temporaryColor;

    public GameManager_ObjectGame gameManager;

    [SerializeField] GameObject[] prefabs_Connection = new GameObject[3];
    [SerializeField] GameObject[] prefabs_Int = new GameObject[3];
    [SerializeField] GameObject[] prefabs_Bool = new GameObject[3];
    [SerializeField] GameObject[] prefabs_Char = new GameObject[3];

    public bool canSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
        gameManager = FindObjectOfType<GameManager_ObjectGame>();
    }

    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameManager.isTemporaryItemDragging)
        {
            if (collision.GetComponent<TemporaryBlock>().emptyBlock != null && canSelect)
            {
                canSelect = false;
                gameManager.isTemporaryItemDragging = false;
                TemporaryBlock b = collision.GetComponent<TemporaryBlock>();

                Cursor.visible = true;
                GameObject g= new GameObject();
                switch (b.inventoryReference.blockType)
                {
                    case InventorySelection.BlockType.Connection:
                        g = Instantiate(prefabs_Connection[b.dir], new Vector3(0f, 0f, 0f), Quaternion.identity);
                        break;

                    case InventorySelection.BlockType.Integer:
                        g = Instantiate(prefabs_Int[b.dir], new Vector3(0f, 0f, 0f), Quaternion.identity);
                        g.GetComponent<IntBlock>().SetIntValue(b.inventoryReference.intValue);
                        break;

                    case InventorySelection.BlockType.Boolean:
                        g = Instantiate(prefabs_Bool[b.dir], new Vector3(0f, 0f, 0f), Quaternion.identity);
                        g.GetComponent<BoolBlock>().SetBoolValue(b.inventoryReference.boolValue);
                        break;

                    case InventorySelection.BlockType.Char:
                        g = Instantiate(prefabs_Char[b.dir], new Vector3(0f, 0f, 0f), Quaternion.identity);
                        g.GetComponent<CharBlock>().SetCharValue(b.inventoryReference.charValue);
                        break;
                }

                g.gameObject.GetComponent<GridBlock>().inventoryReference = b.inventoryReference;
                Destroy(b.gameObject);
                g.transform.parent = transform.parent;
                g.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


                gameManager.trash.SetActive(false);
                Destroy(this.gameObject);
            }
        }

    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TemporaryItem"))
        {
            if (gameManager.isTemporaryItemDragging) { image.color = temporaryColor; collision.GetComponent<TemporaryBlock>().emptyBlock = this; }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TemporaryItem"))
        {
            if (gameManager.isTemporaryItemDragging) { image.color = imageColor; collision.GetComponent<TemporaryBlock>().emptyBlock = null; }
        }
    }
    /*

    private void OnMouseEnter()
    {
        if (gameManager.isTemporaryItemDragging ) { image.color = temporaryColor;  }
    }

    private void OnMouseExit()
    {
        if (gameManager.isTemporaryItemDragging ) {image.color = imageColor; }
    }
    */
}
