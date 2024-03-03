using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class OldManAnimator : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audio;
    [SerializeField] private DialogStarter Dialog;
    private static readonly int DialogParameter = Animator.StringToHash("dialog");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Dialog.GetDialogOpen() && !Dialog.GetDialogFinished())
        {
            _animator.SetBool(DialogParameter, true);
        }
        else
        {
            _animator.SetBool(DialogParameter, false);
            if (Dialog.GetDialogFinished() && _audio.isPlaying)
            {
                _audio.Stop();
            }
        }
    }
}
