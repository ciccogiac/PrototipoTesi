using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class Attribute
{
    public string attributeName;
    public string attributeValue;
} 

[Serializable]
public class Methos
{
    public Method method;
    public List<string> attributes;
}

[Serializable]
public class OggettoEscapeNotPrintedValue : MonoBehaviour
{
    public List<Attribute> attributes;
    public List<Methos> methods;
}

