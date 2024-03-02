using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escape.Levels.Level3.Robot2
{
    [RequireComponent(typeof(Animator))]
    public class Robot2Controller : MonoBehaviour
    {
        private GameObject _player;
        private PlayerInput _input;
        private Animator _animator;
        [SerializeField] private DialogStarter Dialog;
        [SerializeField] private CinemachineBrain CinemachineBrain;
        [SerializeField] private GameObject ClassDesign;
        [SerializeField] private Transform SwitchCamera;
        private bool _doneWithFirstDialogStuff;
        private static readonly int Talking = Animator.StringToHash("Talking");
        private Quaternion _startingRotation;
        private bool _animationFinished = true;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _input = _player.GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _animator.SetBool(Talking, true);
            _startingRotation = transform.localRotation;
            transform.LookAt(SwitchCamera);
        }
        private void Update()
        {
            if (!_animationFinished)
            {
                _input.enabled = false;
            }
            if (!_doneWithFirstDialogStuff)
            {
                if (Dialog.GetDialogFinished())
                {
                    if (!CinemachineBrain.IsBlending)
                    {
                        _animationFinished = false;
                        StartCoroutine(TurnPlayerToClassDesign());
                        _animator.SetBool(Talking, false);
                        transform.localRotation = _startingRotation;
                        _doneWithFirstDialogStuff = true;
                    }
                }
            }
        }
        private IEnumerator TurnPlayerToClassDesign()
        {
            const double duration = 0.5f;
            _input.enabled = false;
            var startRotationPlayer = _player.transform.rotation;
            var classDesignPos = ClassDesign.transform.position;
            var rotRPlayer = classDesignPos - _player.transform.position;
            rotRPlayer.y = 0.0f;
            var targetRotationPlayer = Quaternion.LookRotation(rotRPlayer);
            for (var timePassed = 0.0f; timePassed < duration; timePassed += Time.deltaTime)
            {
                var factor = timePassed / duration;
                _player.transform.rotation = Quaternion.Slerp(startRotationPlayer, targetRotationPlayer, (float)factor);
                yield return null;
            }
            _player.transform.rotation = targetRotationPlayer;
            _animationFinished = true;
            _input.enabled = true;
        }
    }
}
