using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private Sprite Image;

    public string GetName()
    {
        return Name;
    }

    public Sprite GetImage()
    {
        return Image;
    }
}
