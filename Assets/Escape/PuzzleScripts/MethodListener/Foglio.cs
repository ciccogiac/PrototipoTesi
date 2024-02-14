using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foglio : MethodListener
{
    [SerializeField] MeshRenderer paper;

    [SerializeField] Material m;

    public override bool Method(List<(string, string)> objectValue)
    {
        foreach (var value in attributeValueListener)
        {
            if (value.className == className)
            {
                
            }
            else
                return false;
        }

        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        Debug.Log("Foglio Tagliato");
        paper.material = m;
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
    }
}
