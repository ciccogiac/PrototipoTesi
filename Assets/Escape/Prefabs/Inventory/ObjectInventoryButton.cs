using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInventoryButton : MonoBehaviour
{
    private OggettoEscapeValue oggetto;
    private string description;


    InventoryLoad inventoryLoad;
    public void InitializeObjectInventory(OggettoEscapeValue o, InventoryLoad inventory)
    {
        oggetto = o;
        inventoryLoad = inventory;
    }

    public void ShowDescription()
    {
        inventoryLoad.ActivateObjectPanel(oggetto);
    }
}
