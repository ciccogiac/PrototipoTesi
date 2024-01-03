using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject emptyBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestoreEmptyBlock()
    {
        GridBlock gridBlock = GetComponentInChildren<GridBlock>();

        if (gridBlock != null) {
            gridBlock.inventoryReference.CancelDeleteItem();
            Destroy(gridBlock.gameObject);
            GameObject g = Instantiate(emptyBlock, new Vector3(0f, 0f, 0f), Quaternion.identity);
            g.transform.parent = transform;
            g.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}
