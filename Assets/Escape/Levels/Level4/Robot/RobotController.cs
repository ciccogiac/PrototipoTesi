using System;
using UnityEngine;

namespace Escape.Levels.Level4.Robot
{
    public class RobotController : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private DialogStarter Dialog1;
        private bool _dialog1Trigger;
        private bool _longSpeechVisible;
        private static readonly int Talk = Animator.StringToHash("Talk");
        [SerializeField] private AudioClip JoggingSound;
        [SerializeField] private AudioSource AudioSource;

        private void Start()
        {
            if (Dialog1._dialogUsed)
            {
                _dialog1Trigger = true;
                transform.Rotate(Vector3.up, -20);
                Animator.SetBool(Talk, false);
                AudioSource.clip = JoggingSound;
                AudioSource.Play();
            }
        }

        private void Update()
        {
            if (_longSpeechVisible != LongSpeech.LongSpeechOnScreen)
            {
                _longSpeechVisible = LongSpeech.LongSpeechOnScreen;
                if (_longSpeechVisible) AudioSource.Stop();
                else AudioSource.Play();
            }
            if (!_dialog1Trigger)
            {
                if (Dialog1.GetDialogFinished())
                {
                    transform.Rotate(Vector3.up, -20);
                    Animator.SetBool(Talk, false);
                    _dialog1Trigger = true;
                    AudioSource.clip = JoggingSound;
                    AudioSource.Play();
                }
            }
        }
    }
}
