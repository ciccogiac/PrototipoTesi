using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ObjectGame : MonoBehaviour
{
    public GridBlock selectedBlock;
    private bool isABlockSelected = false;

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
        block.isSelected = true;
    }
}
