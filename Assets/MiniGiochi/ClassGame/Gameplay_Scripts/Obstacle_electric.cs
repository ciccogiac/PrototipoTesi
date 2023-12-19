using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle_electric : MonoBehaviour
{
    [SerializeField] float secondToShowGameover = 5f;
    [SerializeField] GameObject canvas_Gameover;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line") || collision.CompareTag("Arrow_Pointer"))
        {
            StartCoroutine(ShowCompilationResult(secondToShowGameover));
        }
    }

    IEnumerator ShowCompilationResult(float time)
    {
        canvas_Gameover.SetActive(true);
        yield return new WaitForSeconds(time);

        string nomeScenaCorrente = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeScenaCorrente);
    }
}
