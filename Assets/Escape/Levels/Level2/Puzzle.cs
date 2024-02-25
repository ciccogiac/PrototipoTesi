using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private PlayerInput Input;
        [SerializeField] private SwitchCameraObject SwitchCameraObject;
        [SerializeField] private GameObject ObjectCallMethodCanvas;


        public override bool Method(List<(string, string)> objectValue)
        {
            ApplyMethod();
            return true;
        }

        public override void ApplyMethod()
        {
            //DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
            //Printer3D.gameObject.SetActive(true);
            //GameManager.printer = Printer3D;
            //LevelHint.nextHint(HintNumber);
            //Teoria.isActive = true;
            PuzzleCanvas.SetActive(true);

            IEnumerator WaitForCallMethodCanvasToDeactivate()
            {
                yield return new WaitUntil(() => !ObjectCallMethodCanvas.activeSelf);
                GameManager.isSeeing = false;
                Input.enabled = true;
                Input.SwitchCurrentActionMap("Puzzle");
            }

            StartCoroutine(WaitForCallMethodCanvasToDeactivate());
        }
    }
}
