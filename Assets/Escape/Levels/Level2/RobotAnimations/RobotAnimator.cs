using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class RobotAnimator : MonoBehaviour
{
    [SerializeField] private DialogStarter Dialog;
    [SerializeField] private DialogStarter SecondDialog;
    [SerializeField] private AudioClip RobotNoiseSound;
    [SerializeField] private AudioClip RobotTalkingSound;
    [SerializeField] private LevelHint LevelHint;
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isTalking;
    private static readonly int Talking = Animator.StringToHash("Talking");
    private static readonly int Walking = Animator.StringToHash("Walking");

    private bool isStart = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isStart)
        {
            isStart = false;
            if (LevelHint.hintCounter >= 2)
            {
                var transform1 = transform;
                var position = transform1.position;
                position = new Vector3(14.7f, position.y, position.z);
                transform1.position = position;
            }
        }

        if (LevelHint.hintCounter < 2)
        {
            if (Dialog.GetDialogOpen() && !_isTalking)
            {
                _isTalking = true;
                _animator.SetBool(Talking, true);
                _audioSource.clip = RobotTalkingSound;
                _audioSource.Play();
            }
            else if (_isTalking && Dialog.GetDialogFinished())
            {
                _isTalking = false;
                _audioSource.clip = RobotNoiseSound;
                _audioSource.Play();
                _animator.SetBool(Talking, false);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                _animator.SetBool(Walking, true);

                IEnumerator WaitForRobotToBeInPosition()
                {
                    yield return new WaitUntil(() => transform.position.x >= 14.7f);
                    _animator.SetBool(Walking, false);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    yield return new WaitUntil(() => SecondDialog.GetDialogOpen());
                    _animator.SetBool(Talking, true);
                    _audioSource.clip = RobotTalkingSound;
                    _audioSource.Play();
                    yield return new WaitUntil(() => SecondDialog.GetDialogFinished());
                    _audioSource.clip = RobotNoiseSound;
                    _audioSource.Play();
                    _animator.SetBool(Talking, false);
                }

                StartCoroutine(WaitForRobotToBeInPosition());
            }
        }
        else
        {
            switch (_isTalking)
            {
                case false when SecondDialog.GetDialogOpen():
                    _isTalking = true;
                    _animator.SetBool(Talking, true);
                    _audioSource.clip = RobotTalkingSound;
                    _audioSource.Play();
                    break;
                case true when SecondDialog.GetDialogFinished():
                    _isTalking = false;
                    _animator.SetBool(Talking, false);
                    _audioSource.clip = RobotNoiseSound;
                    _audioSource.Play();
                    break;
            }
        }
    }
}
