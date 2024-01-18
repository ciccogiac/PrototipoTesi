using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MethodListener
{
    [SerializeField] Animator door;

    [SerializeField] List<MethodListener> methodsListenerToRead;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }


    public override bool Method(List<(string, string)> objectValue)
    {
        // Script per aprire la porta leggendo solo i valori dell'oggetto chiamante
        /*
        if(base.Method(objectValue))
        {
            door.SetBool("character_nearby", true);
            return true;
        }
        return false;
        */
        foreach (var value in attributeValueListener)   
        {
            bool found = false;
            foreach (var m in methodsListenerToRead)
            {
                if (m.objectAttributeValue != null && value.className == m.className)
                {
                    (string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value );
                    if (tupla != (null, null))
                    {
                        found = true;
                        continue;
                    }
                }
            }

            if (!found)
            {
                return false;
            }

           
        }

        door.SetBool("character_nearby", true);
        return true;
    }
}
