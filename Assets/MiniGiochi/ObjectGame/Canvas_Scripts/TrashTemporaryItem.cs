using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashTemporaryItem : MonoBehaviour
{
    public bool isOnTrash = false;
    public bool canDelete = false;

    [SerializeField] Image image;
    [SerializeField] Color imageColor;
    [SerializeField] Color temporaryColor;

    [SerializeField] GameManager_ObjectGame gameManager;

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
                gameManager._audioSource.clip = gameManager.deleteSound;
                gameManager._audioSource.Play();
                gameObject.SetActive(false); 
            }
        }

    }

    private void OnEnable()
    {
        gameManager._audioSource.clip = gameManager.selectSound;
        gameManager._audioSource.Play();
    }

}
