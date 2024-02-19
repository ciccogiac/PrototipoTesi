using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telecomando : MethodListener
{
    [SerializeField] GameObject Tv_Panel;
    [SerializeField] Clue teoria;

    [SerializeField] LevelHint _levelHint;
    [SerializeField] int _hintNumber;

    public override bool Method(List<(string, string)> objectValue)
    {

        foreach (var value in attributeValueListener)
        {
            (string, string) tupla = objectValue.Find(x => x.Item1 == value.attribute);
            if (tupla != (null, null))
            {
                if (tupla.Item2 != value.value)
                {
                    Tv_Panel.SetActive(true);
                    return false;
                }
            }
            else
            {
                Tv_Panel.SetActive(true);
                return false;
            }



        }


        ApplyMethod();
        return true;
    }

    public override void ApplyMethod()
    {
        Debug.Log("CanaleTrovato");
        Tv_Panel.SetActive(false);
        teoria.isActive = true;
        DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        _levelHint.nextHint(_hintNumber);

    }
}
