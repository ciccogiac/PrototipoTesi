using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionInventoryButton : MonoBehaviour
{
    private Clue.ClueType type;
    private string name;
    private string description;


    InventoryLoad inventoryLoad;
    public void InitializeDescriptionInventory(Clue.ClueType cluetype , string clueName , string cluedescription, InventoryLoad inventory)
    {
        type = cluetype;
        name = clueName;
        description = cluedescription;
        inventoryLoad = inventory;
    }

    public void ShowDescription()
    {
        inventoryLoad.ActivateDescriptionPanel(type.ToString(),name,description);
    }
}
