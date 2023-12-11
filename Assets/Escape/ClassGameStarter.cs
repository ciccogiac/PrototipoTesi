using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassGameStarter : MonoBehaviour
{
    [SerializeField] string ClassName;
    [SerializeField] public Dictionary<string, string> coppie = new Dictionary<string, string>();
    [SerializeField] Inventario inventary;

    private void Start()
    {
        coppie.Add("Ciao", "Saluto()");
        coppie.Add("Nome", "GetNome()");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("ClassName", ClassName);
            DatiPersistenti.istanza.methods = inventary.methods ;
            DatiPersistenti.istanza.attributes = inventary.attributes;
            DatiPersistenti.istanza.coppie = coppie;
            SceneManager.LoadScene("ClassGame");
        }
    }
}
