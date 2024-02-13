using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassaforte : MethodListener
{
    [SerializeField] Animator door;
    [SerializeField] SwitchCameraObject cameraSwitch;

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

        Debug.Log("CassaforteAperta");
        cameraSwitch.Interact();
        door.SetBool("character_nearby", true);
        return true;
    }
}
