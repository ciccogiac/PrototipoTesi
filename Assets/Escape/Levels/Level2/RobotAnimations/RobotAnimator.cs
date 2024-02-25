using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class RobotAnimator : MonoBehaviour
{
    [SerializeField] private DialogStarter Dialog;
    [SerializeField] private GameObject SecondDialog; 
    private Animator _animator;
    private bool _isTalking;
    private static readonly int Talking = Animator.StringToHash("Talking");
    private static readonly int Walking = Animator.StringToHash("Walking");

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
            transform.Rotate(Vector3.up, -84.35f);
            _animator.SetBool(Walking, true);
            IEnumerator WaitForRobotToBeInPosition()
            {
                yield return new WaitUntil(() => transform.position.x >= 14.64f);
                _animator.SetBool(Walking, false);
                transform.Rotate(Vector3.up, 84.35f);
                SecondDialog.SetActive(true);
            }
            StartCoroutine(WaitForRobotToBeInPosition());
        }
    }
}
