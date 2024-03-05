using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject EndLevelCanvas;
    [SerializeField] private TMP_Text CaricamentoTesto;
    private const string LastLevelText = "Per ora e' tutto!";
    [SerializeField] private bool IsLastScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsLastScene)
            {
                CaricamentoTesto.text = LastLevelText;
                StartCoroutine(WaitForDurationThenExit(2));
            }
            else
            {
                EndLevelCanvas.SetActive(true);
                Inventario.istanza.SvuotaInventario();
                DatiPersistenti.istanza.SvuotaDatiPersistenti();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
        }
    }

    private IEnumerator WaitForDurationThenExit(float duration)
    {
        EndLevelCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        Inventario.istanza.SvuotaInventario();
        DatiPersistenti.istanza.SvuotaDatiPersistenti();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
