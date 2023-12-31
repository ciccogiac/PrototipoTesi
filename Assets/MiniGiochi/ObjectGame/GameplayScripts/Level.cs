using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string attributeName;
    public GameManager_ObjectGame.AttributeType attributeType;

    public int AttributeIntTarget = 0;
    public bool AttributeBoolTarget = false;
    public string AttributeStringTarget = "";

    public GameObject gameGrid;
    public GameObject inventory;

    private void OnDisable()
    {
        gameGrid.SetActive(false);
        inventory.SetActive(false);
    }

    private void OnEnable()
    {
        gameGrid.SetActive(true);
        inventory.SetActive(true);
    }

}
