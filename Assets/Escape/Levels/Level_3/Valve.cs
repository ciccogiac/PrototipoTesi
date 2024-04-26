using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Valve : MethodListener
{
    [SerializeField] Monitor monitor;


    public override bool MethodInput(List<(string, string)> objectValue, List<(string, string)> inputValue)
    {
        char verso = ' ';
        int quantity = 0;
        if (inputValue[0].Item1 == "sequenzaRotazione")
        {
            var valueInserted = inputValue[0].Item2;
            if (valueInserted.All(c => c is '+' or '-'))
            {
                foreach (var c in valueInserted)
                {
                    switch (c)
                    {
                        case '+':
                            quantity++;
                            break;
                        case '-':
                            quantity--;
                            break;
                    }
                }
                verso = quantity >= 0 ? '+' : '-';
            }
            else
            {
                monitor.SetError("Input errato, prova ad inserire una sequenza di '+' (senso orario) e '-' (senso antiorario) per girare la valvola");
                return false;
            }
            /*if (inputValue[0].Item2 == "+" || inputValue[0].Item2 == "-")
            {
                verso = inputValue[0].Item2[0];
            }
            else
            {
                monitor.SetError("Tipo rotazione errato , prova ad inserire una sequenza di '+' (senso orario) e '-' (senso antiorario) per girare la valvola");
                return false;
            }*/
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
        Ruota(verso, quantity);

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

    public void Ruota(char verso, int quantity)
    {
        (string, string) s = GetComponentInChildren<RuotaValvola>().Ruota(verso, quantity);
        List<(string, string)> l = new List<(string, string)>();
        l.Add(("Valore", s.Item1));
        l.Add(("Colore", s.Item2));
        monitor.SetClass("Valvola");
        monitor.Getter(l);
    }

}
