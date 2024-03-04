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
            if (_level == Levels.Length) PuzzleCompleted();
            else Levels[_level].SetActive(true);
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
