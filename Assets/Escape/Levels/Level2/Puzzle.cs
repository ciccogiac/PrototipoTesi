using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level2
{
    public class Puzzle : MethodListener
    {
        [SerializeField] private Printer3DController Printer3D;
        [SerializeField] private GameManager_Escape GameManager;
        [SerializeField] private LevelHint LevelHint;
        [SerializeField] private int HintNumber;
        [SerializeField] private Clue Teoria;
        [SerializeField] private GameObject PuzzleCanvas;

        public override bool Method(List<(string, string)> objectValue)
        {
            ApplyMethod();
            return true;
        }

        public override void ApplyMethod()
        {
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
            Printer3D.gameObject.SetActive(true);
            GameManager.printer = Printer3D;
            LevelHint.nextHint(HintNumber);
            Teoria.isActive = true;
            PuzzleCanvas.SetActive(true);
        }
    }
}
