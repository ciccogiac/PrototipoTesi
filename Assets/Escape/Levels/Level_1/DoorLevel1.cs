using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevel1 : MethodListener
{
    [SerializeField] string classValueListener;
    public override bool Method(List<(string, string)> objectValue)
    {
        if (className != classValueListener)
            return false;
        ApplyMethod();
        return true;
    }
    public override void ApplyMethod()
    {
        Debug.Log("Porta aperta");
        
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
    }
}
