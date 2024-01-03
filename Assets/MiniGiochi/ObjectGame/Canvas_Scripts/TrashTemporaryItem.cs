using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashTemporaryItem : MonoBehaviour
{
    public bool isOnTrash = false;
    public bool canDelete = false;

    private Image image;
    private Color imageColor;
    [SerializeField] Color temporaryColor;

    private void Start()
    {
        image = GetComponent<Image>();
        imageColor = image.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TemporaryItem"))
        {
            isOnTrash = true;
            image.color = temporaryColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("TemporaryItem"))
        {
            isOnTrash = false;
            image.color = imageColor;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("TemporaryItem"))
        {
            if (canDelete) {
                Cursor.visible = true;  
                collision.gameObject.GetComponent<TemporaryBlock>().inventoryReference.CancelDeleteItem();
                Destroy(collision.gameObject);

                canDelete = false;
                isOnTrash = false;
                gameObject.SetActive(false); }
        }

    }

    private void OnMouseDown()
    {
       // if (isOnTrash) {  canDelete = true;  }
    }
}
