using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntBlock : GridBlock
{
    [SerializeField] int value;
    [SerializeField] TextMeshPro attributeValue_text;

    [SerializeField] GameObject gameObject_Value;

    private void OnValidate()
    {
        WriteIntValue();

    }
    private void WriteIntValue()
    {
        if (value > 0) { attributeValue_text.text = "+" + value.ToString(); }
        else { attributeValue_text.text = value.ToString(); }
    }
    public void SetIntValue(int _value) { value = _value; WriteIntValue(); }
    public override void ConnectBlock()
    {
        base.ConnectBlock();
        base.gameManager.CalculateAttributeValue(value);
    }

    public override void DisconnectBlock()
    {
        base.DisconnectBlock();
        base.gameManager.CalculateAttributeValue(-value);
    }

    public override void RotateBlock(bool isLeftRotation)
    {
        
        gameObject_Value.transform.parent = null;
        base.RotateBlock(isLeftRotation);
        gameObject_Value.transform.parent = transform;
        gameObject_Value.transform.localPosition = Vector3.zero;
    }
}
