using System;
using UnityEngine;
[Serializable]
public class Message
{
    [SerializeField] [TextArea(3, 10)] private string MessageText;
    [SerializeField] private Character Character;

    public string GetMessageText()
    {
        return MessageText;
    }

    public Character GetCharacter()
    {
        return Character;
    }
}