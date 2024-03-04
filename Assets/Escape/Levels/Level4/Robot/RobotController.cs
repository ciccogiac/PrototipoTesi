using System;
using UnityEngine;

namespace Escape.Levels.Level4.Robot
{
    public class RobotController : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private DialogStarter Dialog1;
        private bool _dialog1Trigger;
        private static readonly int Talk = Animator.StringToHash("Talk");

        private void Update()
        {
            if (!_dialog1Trigger)
            {
                if (Dialog1.GetDialogFinished())
                {
                    Animator.SetBool(Talk, false);
                    _dialog1Trigger = true;
                }
            }
        }
    }
}
