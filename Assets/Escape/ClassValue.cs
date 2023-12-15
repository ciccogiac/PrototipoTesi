using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClassValue
{
    public string className;
    public List<AttributeValue> attributes;

}

[Serializable]
public class AttributeValue
{
    public string attribute;
    public bool visibility;
    public List<string> methods;

}
