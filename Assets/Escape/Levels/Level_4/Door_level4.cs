using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_level4 : MethodListener
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



                            else if (tupla.Item1 != null)
                            {
                                doorMonitor.SetError("Attributo : " + value.attribute + " ha un valore errato");
                                ChangeTubeColor("Error");
                                found = false;
                                correctValue = false;
                                continue;
                            }
                            else
                            {
                                doorMonitor.SetError("Non riesco a leggere il valore di : " + value.attribute + " . Si prega di chiamare il metodo corretto");
                                ChangeTubeColor("Error");
                                found = false;
                                correctValue = false;
                                continue;
                            }
                        }


                        else
                        {
                            Debug.Log("Attributo : " + value.attribute + "Non accessibile perch� private");
                            doorMonitor.SetError("Attributo : " + value.attribute + "Non accessibile perch� private");
                            ChangeTubeColor("Error");
                            correctValue = false;
                            continue;
                        }
                    }

                    else //� un attrbiuteNotPrinted
                    {
                        (string, string) tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 == value.value);
                        if (tupla != (null, null))
                        {
                            found = true;
                            continue;
                        }

                        else
                        {
                            tupla = m.objectAttributeValue.Find(x => x.Item1 == value.attribute && x.Item2 != value.value);
                            if (tupla != (null, null))
                            {
                                doorMonitor.SetError("Attributo : " + value.attribute + " ha un valore errato");
                                ChangeTubeColor("Error");
                                found = false;
                                correctValue = false;
                                continue;
                            }
                        }
                    }
                }
            }

            if (!found)
            {
                if (correctValue) doorMonitor.SetError("Nessuno oggetto della classe  " + value.className + " trovato");
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
