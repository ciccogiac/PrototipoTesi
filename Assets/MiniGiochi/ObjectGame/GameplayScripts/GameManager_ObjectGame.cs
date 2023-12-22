using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ObjectGame : MonoBehaviour
{
    public GridBlock selectedBlock;
    public bool isABlockSelected = false;

    [SerializeField] GameObject ButtonDeselectBlock;
    [SerializeField] GameObject ButtonsRotation;

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
