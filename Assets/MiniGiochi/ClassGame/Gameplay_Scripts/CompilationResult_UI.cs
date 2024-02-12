using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*Gestisce la ui della fase di compilazione . In caso di compilazione positiva chiama il metodo returntoescape di gamemanager per ricaricare la scena di escape.
 */
public class CompilationResult_UI : MonoBehaviour
{
    [SerializeField] string correctCompilation="Compilazione avvenuta correttamente!";
    [SerializeField] string wrongCompilation = "Errore di compilazione";

    [SerializeField] Color correctColor;
    [SerializeField] Color wrongColor;
    [SerializeField] Color hideColor;

    [SerializeField]  Image compilerImage;


    [SerializeField] TextMeshProUGUI compilationText;
    [SerializeField] GameManager_ClassGame gameManager;

    [SerializeField] GameObject buttonCompile;

    [SerializeField] float secondsToShowResult = 3f;



    public void CorrectCompilation()
    {

        compilationText.text = correctCompilation;
        compilerImage.color = correctColor;

        StartCoroutine(ShowCompilationResult(secondsToShowResult,true));
    }

    public void WrongCompilation()
    {

        compilationText.text = wrongCompilation;
        compilerImage.color = wrongColor;

        StartCoroutine(ShowCompilationResult(secondsToShowResult,false));

    }

    public void Compile(bool isCorrectCode)
    {
        if (isCorrectCode) { CorrectCompilation(); }
        else WrongCompilation();
    }

    IEnumerator ShowCompilationResult(float time,bool compiled)
    {
        buttonCompile.SetActive(false);
        yield return new WaitForSeconds(time);
        compilationText.text = "";
        compilerImage.color = hideColor;
        
        if (compiled) gameManager.ReturnToescape(true);
        else
            buttonCompile.SetActive(true);


    }
}
