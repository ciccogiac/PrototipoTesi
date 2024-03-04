using System;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level4.QuadroMethodPuzzle
{
    public class QuadroStarter : MethodListener
    {
        [SerializeField] private QuadroMethodPuzzleManager QuadroMethodPuzzleManager;
        //[SerializeField] private Outline ParentOutline;

        public override void Start()
        {
            if (DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID))
            {
                QuadroMethodPuzzleManager.RevealMetodo();
            }
        }

        /*private void Update()
        {
            var outline = GetComponent<Outline>();
            if (outline != null && outline.enabled)
                ParentOutline.enabled = true;
            else
                ParentOutline.enabled = false;
        }*/

        public override void Getter(List<(string, string)> objectValue)
        {
            QuadroMethodPuzzleManager.NextLevel();
        }
    }
}
