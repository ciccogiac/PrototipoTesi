using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoolBlock : GridBlock
{
    [SerializeField] bool value;
    [SerializeField] TextMeshProUGUI attributeValue_text;

    [SerializeField] GameObject gameObject_Value;

    private void OnValidate()
    {
        attributeValue_text.text = value.ToString();
    }

    public void SetBoolValue(bool _value) { value = _value; attributeValue_text.text = value.ToString(); }
    public override void ConnectBlock()
    {
        base.ConnectBlock();
        base.gameManager.CalculateAttributeValue(value);
    }

    public override void DisconnectBlock()
    {
        base.DisconnectBlock();
        base.gameManager.CalculateAttributeValue(!value);
    }

    public override void RotateBlock(bool isLeftRotation)
    {

        gameObject_Value.transform.parent = null;
        base.RotateBlock(isLeftRotation);
        gameObject_Value.transform.parent = transform;
        gameObject_Value.transform.localPosition = Vector3.zero;
    }
}
