using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Door_Valve : MethodListener
{
    [SerializeField] Animator door;

    [SerializeField] List<MethodListener> methodsListenerToRead;
    [SerializeField] string classValueListener;

    [SerializeField] Monitor doorMonitor;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SetClass(string name)
    {
        base.SetClass(name);
        doorMonitor.SetClass(name);

        if (className != classValueListener)
        {
            doorMonitor.SetError("Classe Errata");
            ChangeTubeColor("Error");
        }
    }

    public override void RemoveObject()
    {
        base.RemoveObject();
        doorMonitor.RemoveObject();
    }

    public override void Getter(List<(string, string)> objectValue)
    {
        base.Getter(objectValue);
        //doorMonitor.Getter(objectValue);
    }

    public override bool Method(List<(string, string)> objectValue)
    {


        if (className != classValueListener)
        {
            doorMonitor.SetError("Classe Errata");
            ChangeTubeColor("Error");
            return false;
        }


        foreach (var value in attributeValueListener)
        {
            bool found = false;
            bool correctValue = true;
            foreach (var m in methodsListenerToRead)
            {


                if (m.objectAttributeValue != null && value.className == m.className)
                {

                        (string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == "Colore" && x.Item2 == value.attribute);
                        if (tupla != (null, null))
                        {
                            (string, string) tupla2 = m.objectAttributeValue.Find(x => x.Item1 == "Valore" && x.Item2 == value.value);
                            if (tupla2 != (null, null))
                            {
                                 found = true;
                                 continue;
                            }

                            else
                            {

                                doorMonitor.SetError("Valori delle valvole errati");
                                ChangeTubeColor("Error");
                                found = false;
                                correctValue = false;
                                continue;
                            }
                        }

                        
                    
                }
            }

            if (!found)
            {
                if (correctValue)doorMonitor.SetError("Valori delle valvole errati");
                ChangeTubeColor("Error");
                return false;
            }


        }

        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        doorMonitor.SetError("");

        ChangeTubeColor("Getter");
        door.SetBool("character_nearby", true);

        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
    }
}
