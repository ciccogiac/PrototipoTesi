using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MethodListener
{
    [SerializeField] Animator door;

    [SerializeField] List<MethodListener> methodsListenerToRead;
    [SerializeField] string classValueListener;

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

        if (className != classValueListener)
            return false;

        foreach (var value in attributeValueListener)   
        {
            bool found = false;
            foreach (var m in methodsListenerToRead)
            {

                
                if (m.objectAttributeValue != null && value.className == m.className)
                {
                    ClassValue classValue = Inventario.istanza.classi.Find(x => x.className == m.className);

                    if (classValue != null)
                    {

                        if (classValue.attributes.Find(x => x.attribute == value.attribute).visibility)
                        {

                            (string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value);
                            if (tupla != (null, null))
                            {
                                found = true;
                                continue;
                            }
                        }

                        else
                        {
                            Debug.Log("Attributo : " + value.attribute + "Non accessibile perchè private");
                            continue;
                        }
                    }

                    else //è un attrbiuteNotPrinted
                    {
                        (string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value);
                        if (tupla != (null, null))
                        {
                            found = true;
                            continue;
                        }
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
