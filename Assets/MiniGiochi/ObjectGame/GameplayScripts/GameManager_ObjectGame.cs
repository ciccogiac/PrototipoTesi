using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_ObjectGame : MonoBehaviour
{
    public GridBlock selectedBlock;
    public bool isABlockSelected = false;
    [SerializeField] GameObject emptyBlock;

    [SerializeField] GameObject ButtonDeselectBlock;
    [SerializeField] GameObject ButtonRemoveBlock;
    [SerializeField] GameObject ButtonsRotation;

    private int attributeValue=0;
    public int AttributeTarget=0;
    [SerializeField] TextMeshProUGUI attributeValue_text;
    [SerializeField] TextMeshProUGUI attributeTarget_text;

    public bool isTemporaryItemDragging = false;
    public GameObject trash;

    [SerializeField] Block[] blocks;


    private void Start()
    {
        attributeTarget_text.text = attributeValue.ToString();
        attributeTarget_text.text = AttributeTarget.ToString();

        trash = FindObjectOfType<TrashTemporaryItem>().gameObject;
        trash.SetActive(false);

        blocks = FindObjectsOfType<Block>();
    }

    public void CalculateAttributeValue(int value) { attributeValue += value; attributeValue_text.text = attributeValue.ToString(); }

    public void VerifyAttributeValue() { if(attributeValue == AttributeTarget) { Debug.Log("Raggiunto il valore target"); } }

    public void Left_BlockRotation()
    {
        if(isABlockSelected) { selectedBlock.RotateBlock(true); }
    }

    public void Right_BlockRotation()
    {
        if (isABlockSelected) { selectedBlock.RotateBlock(false); }
    }

    public void SelectBlock(GridBlock block)
    {
        selectedBlock = block;
        isABlockSelected = true;
        block.SelectBlock();

        ButtonDeselectBlock.SetActive(true);
        ButtonRemoveBlock.SetActive(true);
        ButtonsRotation.SetActive(true);
    }

    public void DeselectBlock()
    {
        selectedBlock.DeselectBlock();
        selectedBlock = null;
        isABlockSelected = false;
       
        ButtonDeselectBlock.SetActive(false);
        ButtonsRotation.SetActive(false);
        ButtonRemoveBlock.SetActive(false);
    }

    public void ResetPath()
    {
        if (!isTemporaryItemDragging)
        {
            foreach (var block in blocks)
            {
                block.RestoreEmptyBlock();
            }

            selectedBlock = null;
            isABlockSelected = false;

            ButtonDeselectBlock.SetActive(false);
            ButtonRemoveBlock.SetActive(false);
            ButtonsRotation.SetActive(false);
        }
    }

    public void RemoveGridBlock()
    {
        GameObject g = Instantiate(emptyBlock, new Vector3(0f, 0f, 0f), Quaternion.identity);
        g.transform.parent = selectedBlock.gameObject.transform.parent;
        selectedBlock.gameObject.GetComponent<GridBlock>().inventoryReference.CancelDeleteItem();
        Destroy(selectedBlock.gameObject);
        g.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        selectedBlock = null;
        isABlockSelected = false;

        ButtonDeselectBlock.SetActive(false);
        ButtonRemoveBlock.SetActive(false);
        ButtonsRotation.SetActive(false);
    }
    public void CloseGame()
    {
        if (!isTemporaryItemDragging)
        {
        }
    }
}
