using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassaforte : MethodListener
{
    [SerializeField] Animator door;

    public override bool Method(List<(string, string)> objectValue)
    {
        foreach (var value in attributeValueListener)
        {
                (string, string) tupla = objectValue.Find(x => x.Item1 == value.attribute);
                if (tupla != (null, null))
                {
                    if (tupla.Item2 != value.value)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            


        }

        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        Debug.Log("CassaforteAperta");
        door.SetBool("character_nearby", true);
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);

    }
}
