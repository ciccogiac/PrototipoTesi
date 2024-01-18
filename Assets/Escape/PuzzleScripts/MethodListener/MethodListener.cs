using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeValueListener
{
    public string className;
    public string attribute;
    public string value;

}

[Serializable]
public class MethodListener : MonoBehaviour
{
    

    public List<AttributeValueListener> attributeValueListener;
    public List<(string, string)> objectAttributeValue;

    public string className;

    public virtual void Getter(List<(string, string)> objectValue)
    {
        objectAttributeValue = objectValue;
    }

    public virtual bool  Method(List<(string, string)> objectValue)
    {
        foreach(var value in attributeValueListener)
        {
            if (value.className == className)
            {
                (string, string) tupla = objectValue.Find(x => x.Item1 == value.attribute);
                if (tupla != (null, null))
                {
                    if (tupla.Item2 != value.value)
                        return false;
                }
                else return false;
            }
            else return false;

        }

        return true;
        
    }

    public virtual void RemoveObject()
    {
        objectAttributeValue = null;
        className = "";
    }
}
