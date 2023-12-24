using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntBlock : GridBlock
{
    [SerializeField] int value;
    [SerializeField] TextMeshProUGUI attributeValue_text;

    private void OnValidate()
    {
        attributeValue_text.text = value.ToString();
    }

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
}
