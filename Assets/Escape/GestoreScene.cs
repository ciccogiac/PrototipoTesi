using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestoreScene : MonoBehaviour
{
    public void ApriNuovaScena(string nomeScena)
    {
        SceneManager.LoadScene(nomeScena);
    }
}
