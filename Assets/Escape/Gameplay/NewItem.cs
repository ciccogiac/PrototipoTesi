using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemType;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    [SerializeField] Image itemImage;
    [SerializeField] Sprite[] images;

    private GameManager_Escape gameManager;
    private bool isReading = false;

  

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager_Escape>();
    }

    public void InitializeItemCanvas(string type,string name,string description)
    {
        itemType.text = type;
        itemName.text = name;
        itemDescription.text = description;

        switch (type)
        {
            case "Teoria":
                itemImage.sprite = images[0];
                break;
            case "Attributo":
                itemImage.sprite = images[1];
                break;
            case "Metodo":
                itemImage.sprite = images[2];
                break;
            case "Classe":
                itemImage.sprite = images[3];
                break;
            case "Oggetto":
                itemImage.sprite = images[4];
                break;
            case "ProgettoClasse":
                itemImage.sprite = images[5];
                break;
        }

        isReading = true;

    }

    private void Update()
    {
        if (isReading && gameManager._input.readObject == true)
        {
            CloseNewItemCanvas();
        }
    }

    private void CloseNewItemCanvas()
    {

        gameManager.DeactivateNewItemCanvas();
        isReading = false;
        gameManager._input.readObject = false;
    }
}
