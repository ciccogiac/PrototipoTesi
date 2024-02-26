using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escape.Levels.Level2
{
    public class Puzzle : MethodListener
    {
        [SerializeField] private GameManager_Escape GameManager;
        [SerializeField] private GameObject PuzzleCanvas;
        [SerializeField] private PlayerInput Input;
        [SerializeField] private SwitchCameraObject SwitchCameraObject;
        [SerializeField] private GameObject ObjectCallMethodCanvas;
        [SerializeField] private GameObject Printer3D;

        [SerializeField] private Texture2D cursorTexture;
        private Vector2 cursorHotspot;


        public override bool Method(List<(string, string)> objectValue)
        {
            ApplyMethod();
            return true;
        }

        public override void ApplyMethod()
        {
            if (DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID))
            {
                Printer3D.SetActive(true);
                SwitchCameraObject.isInteractable = false;
                SwitchCameraObject.isActive = false;
                GameManager.printer = Printer3D.GetComponent<Printer3DController>();
                return;
            }
            
            PuzzleCanvas.SetActive(true);

            IEnumerator WaitForCallMethodCanvasToDeactivate()
            {
                yield return new WaitUntil(() => !ObjectCallMethodCanvas.activeSelf);
                GameManager.isSeeing = false;
                Input.enabled = true;
                Input.SwitchCurrentActionMap("Puzzle");

                cursorHotspot = new Vector2(0f, 0f);
                Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }

            StartCoroutine(WaitForCallMethodCanvasToDeactivate());
        }
    }
}
