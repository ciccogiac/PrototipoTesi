using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager_ObjectGame : MonoBehaviour
{
    public GridBlock selectedBlock;
    public bool isABlockSelected = false;

    [SerializeField] GameObject ButtonDeselectBlock;
    [SerializeField] GameObject ButtonsRotation;

    private int attributeValue=0;
    public int AttributeTarget=0;
    [SerializeField] TextMeshProUGUI attributeValue_text;
    [SerializeField] TextMeshProUGUI attributeTarget_text;


    private void Start()
    {
        attributeTarget_text.text = attributeValue.ToString();
        attributeTarget_text.text = AttributeTarget.ToString();
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
        ButtonsRotation.SetActive(true);
    }

    public void DeselectBlock()
    {
        selectedBlock.DeselectBlock();
        selectedBlock = null;
        isABlockSelected = false;
       
        ButtonDeselectBlock.SetActive(false);
        ButtonsRotation.SetActive(false);
    }

    public void ResetPath()
    {

    }

    public void CloseGame()
    {

    }
}
