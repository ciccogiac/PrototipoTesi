using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInventoryButton : MonoBehaviour
{
    ClassValue classValue;
    InventoryLoad inventoryLoad;
    public void InitializeClassInventoryVisualization(string className, InventoryLoad inventory)
    {
        classValue = Inventario.istanza.classi.Find(x => x.className == className);
        inventoryLoad = inventory;
    }

    public void ShowClassConnections()
    {
        //Debug.Log("Class : " + classValue.className);
        inventoryLoad.ActivateClassPanel(classValue);
    }
}
