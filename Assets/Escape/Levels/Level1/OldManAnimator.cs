using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class OldManAnimator : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audio;
    private bool _longSpeechVisible;
    [SerializeField] private DialogStarter Dialog;
    [SerializeField] private DialogStarter SecondDialog;
    private static readonly int DialogParameter = Animator.StringToHash("dialog");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (_longSpeechVisible != LongSpeech.LongSpeechOnScreen)
        {
            _longSpeechVisible = LongSpeech.LongSpeechOnScreen;
            if (_longSpeechVisible)
            {
                _audio.Pause();
            }
            else
            {
                _audio.Play();
            }
        }
        if (Dialog.GetDialogOpen() && !Dialog.GetDialogFinished())
        {
            if (!_animator.GetBool(DialogParameter))
            {
                _animator.SetBool(DialogParameter, true);
            }
        }
        else if (Dialog.GetDialogFinished())
        {
            if (_animator.GetBool(DialogParameter))
            {
                _animator.SetBool(DialogParameter, false);
            }

            if (Dialog.GetDialogFinished() && _audio.isPlaying)
            {
                _audio.Pause();
            }
        }
        if (SecondDialog.GetDialogOpen() && !SecondDialog.GetDialogFinished())
        {
            if (!_animator.GetBool(DialogParameter))
            {
                _animator.SetBool(DialogParameter, true);
            }

            if (!_audio.isPlaying)
            {
                _audio.Play();
            }
        }
        else if (SecondDialog.GetDialogFinished())
        {
            if (_animator.GetBool(DialogParameter))
            {
                _animator.SetBool(DialogParameter, false);
            }

            if (_audio.isPlaying)
            {
                _audio.Pause();
            }
        }
    }
}
