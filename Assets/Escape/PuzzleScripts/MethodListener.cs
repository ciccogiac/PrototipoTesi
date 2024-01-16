using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeValueListener
{
    public string attribute;
    public string value;

}

[Serializable]
public class MethodListener : MonoBehaviour
{
    [SerializeField] Animator door;

    public List<AttributeValueListener> attributeValueListener;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }

    public void Method(List<(string, string)> objectValue)
    {
        foreach(var value in attributeValueListener)
        {
            (string, string) tupla = objectValue.Find(x => x.Item1 == value.attribute);
            if ( tupla != (null,null))
            {
                if (tupla.Item2 != value.value)
                    return;
            }
            else return;
        }
        door.SetBool("character_nearby", true);
    }
}
