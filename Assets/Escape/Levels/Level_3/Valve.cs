using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MethodListener
{
    [SerializeField] Monitor monitor;

    public override bool Method(List<(string, string)> objectValue)
    {
        Debug.Log("gira la valvola");
        ApplyMethod();
        
        return true;
    }

    public override void ApplyMethod()
    {
        //DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);

        string s = GetComponentInChildren<RuotaValvola>().Ruota();
        List<(string, string)> l = new List<(string, string)>();
        l.Add(("Valore", s));
        monitor.SetClass("Valvola");
        monitor.Getter(l);

    }
}
