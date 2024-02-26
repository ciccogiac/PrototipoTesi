using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialObjectGame : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    int i = 0;

    [SerializeField] Button okButton;
    [SerializeField] Button backButton;

    [SerializeField] GameManager_ObjectGame gameManager;

    public void PreviousItem()
    {
        backButton.interactable = false;
        i--;
        if (i >= 0)
        {
            backButton.gameObject.SetActive(true);

            items[i].SetActive(true);
            items[i + 1].SetActive(false);

            if (i == 0)
                backButton.gameObject.SetActive(false);

        }
        backButton.interactable = true;

    }

    public void NextItem()
    {
        okButton.interactable = false;
        i++;
        if (i < items.Length)
        {
            backButton.gameObject.SetActive(true);
            if (i > 0)
                items[i - 1].SetActive(false);

            items[i].SetActive(true);
            okButton.interactable = true;

        }
        else
        {
            gameManager.EndTutorial();

        }
    }
}
