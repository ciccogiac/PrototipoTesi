using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemporaryBlock : MonoBehaviour
{
    Rigidbody2D rb;
    public InventorySelection inventoryReference;

    [SerializeField] GameObject[] directions = new GameObject[3];

    public int dir = 0;

    private SpriteRenderer image;
    [SerializeField] Color intColor;

    [SerializeField] GameObject valueImage;
    [SerializeField] TextMeshProUGUI value_text;

    private TrashTemporaryItem trash;
    public EmptyBlock emptyBlock;

    private GameManager_ObjectGame gameManager;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        trash = FindObjectOfType<TrashTemporaryItem>();
        gameManager = FindObjectOfType<GameManager_ObjectGame>();

        switch ( inventoryReference.direction)
        {
            case InventorySelection.BlockDirection.Forward:
                dir = 0;
                directions[dir].SetActive(true);
                break;
            case InventorySelection.BlockDirection.Right:
                dir = 1;
                directions[dir].SetActive(true);
                break;
            case InventorySelection.BlockDirection.Left:
                dir = 2;
                directions[dir].SetActive(true);
                break;
        }

        switch (inventoryReference.blockType)
        {
            case InventorySelection.BlockType.Connection:
                valueImage.SetActive(false);
                break;

            case InventorySelection.BlockType.Integer:
                valueImage.SetActive(true);
                image.color = intColor;
                if (inventoryReference.intValue > 0) { value_text.text = "+" + inventoryReference.intValue.ToString(); }
                else { value_text.text = inventoryReference.intValue.ToString(); }    
                break;

            case InventorySelection.BlockType.Boolean:
                valueImage.SetActive(true);
                image.color = intColor;
                value_text.text = inventoryReference.boolValue.ToString();
                break;

            case InventorySelection.BlockType.Char:
                valueImage.SetActive(true);
                image.color = intColor;
                value_text.text = inventoryReference.charValue.ToString();
                break;

        }

        }

    private void OnMouseDown()
    {
        if(trash.isOnTrash) { trash.canDelete = true; }
        else if(emptyBlock!=null) { 
            emptyBlock.canSelect = true;
            gameManager._audioSource.clip = gameManager.selectSound2;
            gameManager._audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        inventoryReference.RestoreImage();
    }

    private void FixedUpdate()
    {
            // Leggi la posizione del mouse
            Vector3 posizioneMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posizioneMouse.z = 0f; // Assicurati che la coordinata Z sia zero poiché stiamo lavorando in 2D

            rb.MovePosition(posizioneMouse);

    }
}
