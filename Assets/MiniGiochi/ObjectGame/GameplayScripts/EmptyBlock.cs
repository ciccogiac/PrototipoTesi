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

    [SerializeField] GameObject[] prefabs = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
        gameManager = FindObjectOfType<GameManager_ObjectGame>();
    }

    private void OnMouseDown()
    {
        if (gameManager.isTemporaryItemDragging)
        {
            TemporaryBlock b = FindObjectOfType<TemporaryBlock>();
            
            gameManager.isTemporaryItemDragging = false;

            Cursor.visible = true;

            GameObject g= Instantiate(prefabs[b.dir], new Vector3(0f,0f,0f), Quaternion.identity);
            g.gameObject.GetComponent<GridBlock>().inventoryReference = b.inventoryReference;
            Destroy(b.gameObject);
            g.transform.parent = transform.parent;
            g.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            g.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            

            gameManager.trash.SetActive(false);
            Destroy(this.gameObject);

        }
    }

    private void OnMouseEnter()
    {
        if (gameManager.isTemporaryItemDragging ) { image.color = temporaryColor;  }
    }

    private void OnMouseExit()
    {
        if (gameManager.isTemporaryItemDragging ) {image.color = imageColor; }
    }
}
