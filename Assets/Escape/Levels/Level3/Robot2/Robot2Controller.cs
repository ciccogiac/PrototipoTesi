using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Escape.Levels.Level3.Robot2
{
    [RequireComponent(typeof(Animator))]
    public class Robot2Controller : MonoBehaviour
    {
        private Animator _animator;
        private bool _longSpeechVisible;
        [SerializeField] private DialogStarter Dialog;
        [SerializeField] private Transform SwitchCamera;
        private bool _doneWithFirstDialogStuff;
        private static readonly int Talking = Animator.StringToHash("Talking");
        private Quaternion _startingRotation;
        [SerializeField] private SphereCollider ClassDesignCollider;
        [SerializeField] private AudioClip TalkingSound;
        [SerializeField] private AudioClip WorkingSound;
        [SerializeField] private AudioSource AudioSource;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Talking, true);
            _startingRotation = transform.localRotation;
            if (!Dialog._dialogUsed)
            {
                transform.LookAt(SwitchCamera, Vector3.up);
                AudioSource.clip = TalkingSound;
                AudioSource.Play();
                if (ClassDesignCollider != null)
                    ClassDesignCollider.enabled = false;
            }
            else
            {
                _doneWithFirstDialogStuff = true;
                _animator.SetBool(Talking, false);
                AudioSource.clip = WorkingSound;
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
            if (!_doneWithFirstDialogStuff)
            {
                if (Dialog.GetDialogFinished())
                {
                    _animator.SetBool(Talking, false);
                    AudioSource.clip = WorkingSound;
                    AudioSource.Play();
                    transform.localRotation = _startingRotation;
                    ClassDesignCollider.enabled = true;
                    _doneWithFirstDialogStuff = true;
                }
            }
        }
    }
}
