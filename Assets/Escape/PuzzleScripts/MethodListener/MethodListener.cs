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

    public MethodTube[] methodTubes ;

    public int methodListenerID ;
    
    public virtual void Start()
    {
       // methodTubes = GetComponentsInChildren<MethodTube>();
    }
    

    private void OnValidate()
    {
        methodTubes = GetComponentsInChildren<MethodTube>();
    }

    public void ChangeTubeColor(string color)
    {
        foreach (var tube in methodTubes) { tube.ChangeTubeColor(color); }
    }

    public virtual void Getter(List<(string, string)> objectValue)
    {
        objectAttributeValue = objectValue;
        ChangeTubeColor("Getter");
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
                    {
                        ChangeTubeColor("Error");
                        return false;
                    }
                }
                else
                {
                    ChangeTubeColor("Error");
                    return false;
                }
            }
            else
            {
                ChangeTubeColor("Error");
                return false;
            }

        }

        return true;
        
    }

    public virtual bool MethodInput(List<(string, string)> objectValue, List<(string, string)> inputValue)
    {
        /*
        Debug.Log("AttributeValue");
        foreach(var a in objectValue)
        {
            Debug.Log(a.Item1 + " " + a.Item2);
        }

        Debug.Log("InputValue");
        foreach (var a in inputValue)
        {
            Debug.Log(a.Item1 + " " + a.Item2);
        }
        */
        return false;
    }

    public virtual void SetClass(string name)
    {
        className = name;
        ChangeTubeColor("Connected");
    }

    public virtual void RemoveObject()
    {
        objectAttributeValue = null;
        className = "";
        ChangeTubeColor("Disconnect");
    }

    public virtual void ApplyMethod() { }
}
