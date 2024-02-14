using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OggettoEscapeValue
{
    public bool isMadeByPrinter = true;

    public string objectName;
    public string className;
    public List<Attribute> attributes;
    public List<Methos> methods;

    public string description;

    public GameObject classPrefab;

    public int ObjectInteractorId = 0;
}

[Serializable] 
public class Attribute
{
    public string attributeName;
    public string attributeValue;

    public Attribute(string name, string value)
    {
        this.attributeName = name;
        this.attributeValue = value;
    }
} 

[Serializable]
public class Methos
{
    public Method method;
    public List<string> attributes;
    private (Method, List<string>) p;

    public Methos(Method m , List<string> l)
    {
        this.method = m;
        this.attributes = l;
    }
}



