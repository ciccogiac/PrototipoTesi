using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class RobotAnimator : MonoBehaviour
{
    [SerializeField] private DialogStarter Dialog;
    private Animator _animator;
    private bool _isTalking;
    private static readonly int Talking = Animator.StringToHash("Talking");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Dialog.GetDialogOpen() && !_isTalking)
        {
            _isTalking = true;
            _animator.SetBool(Talking, true);
        }
        else if (_isTalking && Dialog.GetDialogUsed())
        {
            _isTalking = false;
            _animator.SetBool(Talking, false);
        }
    }
}
