using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MethodListener
{
    [SerializeField] Monitor monitor;


    public override bool MethodInput(List<(string, string)> objectValue, List<(string, string)> inputValue)
    {
        char verso = ' ';
        if (inputValue[0].Item1 == "versoRotazione")
        {
            if (inputValue[0].Item2 == "+" || inputValue[0].Item2 == "-")
            {
                verso = inputValue[0].Item2[0];
            }
            else
            {
                monitor.SetError("Tipo rotazione errato , prova ad inserire + per girare la valvola a destra , o - per girarla a sinistra");
                return false;
            }
        }

            


        foreach (var value in attributeValueListener)
        {
            var tupla = objectValue.Find(x => x.Item1 == value.attribute);
            if (tupla != (null, null))
            {
                if (tupla.Item2 != value.value)
                {
                    monitor.SetClass(value.className);
                    monitor.SetError("Tipo di Valvola non corretta");
                    return false;
                }
            }
            else
            {
                monitor.SetError("Oggetto non riconosciuto");
                return false;
            }



        }

        //Debug.Log("gira la valvola");
        //ApplyMethod();
        Ruota(verso);

        return true;
    }
    /*
    public override bool Method(List<(string, string)> objectValue)
    {

        
        foreach (var value in attributeValueListener)
        {
            var tupla = objectValue.Find(x => x.Item1 == value.attribute);
            if (tupla != (null, null))
            {
                if (tupla.Item2 != value.value)
                {
                    monitor.SetClass(value.className);
                    monitor.SetError("Tipo di Valvola non corretta");
                    Debug.Log("Entro");
                    return false;
                }
            }
            else
            {
                monitor.SetError("Oggetto non riconosciuto");
                return false;
            }



        }
        
        Debug.Log("gira la valvola");
        ApplyMethod();
        
        return true;
    }
    */
    public override void SetClass(string name)
    {
        base.SetClass(name);
        monitor.SetClass(name);


            var tupla = attributeValueListener.Find(x => x.className == name);
            if (tupla == null)
            {
                monitor.SetError("Tipo di Valvola non corretta");
            }


    }

    public override void RemoveObject()
    {
        base.RemoveObject();
        monitor.RemoveObject();
    }

    public void Ruota(char verso)
    {
        (string, string) s = GetComponentInChildren<RuotaValvola>().Ruota(verso);
        List<(string, string)> l = new List<(string, string)>();
        l.Add(("Valore", s.Item1));
        l.Add(("Colore", s.Item2));
        monitor.SetClass("Valvola");
        monitor.Getter(l);
    }

}
