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
        [SerializeField] private DialogStarter Dialog;
        [SerializeField] private Transform SwitchCamera;
        private bool _doneWithFirstDialogStuff;
        private static readonly int Talking = Animator.StringToHash("Talking");
        private Quaternion _startingRotation;
        [SerializeField] private SphereCollider ClassDesignCollider;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(Talking, true);
            _startingRotation = transform.localRotation;
            transform.LookAt(SwitchCamera, Vector3.up);
            if (ClassDesignCollider != null)
                ClassDesignCollider.enabled = false;
        }
        private void Update()
        {
            if (!_doneWithFirstDialogStuff)
            {
                if (Dialog.GetDialogFinished())
                {
                    _animator.SetBool(Talking, false);
                    transform.localRotation = _startingRotation;
                    ClassDesignCollider.enabled = true;
                    _doneWithFirstDialogStuff = true;
                }
            }
        }
    }
}
