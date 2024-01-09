using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDictionary : MonoBehaviour
{


    [SerializeField]
    private List<ClassValue> classi = new List<ClassValue>();


    [SerializeField] public Dictionary<string, (bool, List<string>)> coppie;
    // Start is called before the first frame update
    
    public Dictionary<string, (bool, List<string>)> FindClass(string _className)
    {
       ClassValue classFound = classi.Find(s => string.Equals(s.className, _className, StringComparison.OrdinalIgnoreCase));

        if (classFound != null)
        {
            //Debug.Log("Classe  '" +  classFound.className +"' trovata" );
            return CreateDictionary(classFound);
        }
        else
        {
            //Debug.Log("Classe  '" + _className + "' non trovata");
            return null;
        }
    }

    public Dictionary<string, (bool, List<string>)> CreateDictionary(ClassValue _class)
    {
        Dictionary<string, (bool, List<string>)>  dizionario = new Dictionary<string, (bool, List<string>)>();

        foreach (var attribute in _class.attributes)
        {
            dizionario.Add(attribute.attribute, (attribute.visibility, attribute.methods));
        }

        return dizionario;
    }
}
