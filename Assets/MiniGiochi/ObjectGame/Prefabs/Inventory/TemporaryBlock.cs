using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBlock : MonoBehaviour
{
    Rigidbody2D rb;
    public InventorySelection inventoryReference;

    [SerializeField] GameObject[] directions = new GameObject[3];

    public int dir = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        switch( inventoryReference.direction)
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
