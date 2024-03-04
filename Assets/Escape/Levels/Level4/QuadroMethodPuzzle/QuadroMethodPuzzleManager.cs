using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Escape.Levels.Level4.QuadroMethodPuzzle
{
    public class QuadroMethodPuzzleManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Levels;
        [SerializeField] private MethodListener Starter;
        [SerializeField] private GameObject Metodo;
        private int _level;

        [UsedImplicitly]
        public void NextLevel()
        {
            var outline = GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;
            _level++;
            Levels[_level - 1].SetActive(false);
            Levels[_level].SetActive(true);
            if (_level == Levels.Length - 1) PuzzleCompleted();
        }

        private void PuzzleCompleted()
        {
            Levels[0].SetActive(true);
            Levels[^1].SetActive(false);
            _level = 0;
            DatiPersistenti.istanza.methodsListeners.Add(Starter.methodListenerID);
            RevealMetodo();
        }

        public void RevealMetodo()
        {
            Starter.gameObject.tag = "Untagged";
            Metodo.SetActive(true);
        }
    }
}
