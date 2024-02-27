using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Escape.Levels.Level2
{
    public class PrinterAnimation : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain CinemachineBrain;
        [SerializeField] private GameObject FlashEffect;
        [SerializeField] private SwitchCameraObject SwitchCameraObject;
        private GameObject _player;
        private PlayerInput _input;
        public bool PuzzleCompleted;
        private bool _triggered;
        private bool animationFinished = false;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _input = _player.GetComponent<PlayerInput>();
        }
        private void Update()
        {
            if (!animationFinished)
            {
                _input.enabled = false;
            }
           

            if (!_triggered && PuzzleCompleted && !CinemachineBrain.IsBlending && SwitchCameraObject.GetComponent<BoxCollider>().enabled)
            {
                _triggered = true;
                StartCoroutine(TurnPlayerAndScalePrinter());
            }
        }

        private IEnumerator TurnPlayerAndScalePrinter()
        {
            const double duration = 1.0;
            //_player = GameObject.FindWithTag("Player");
            _input.enabled = false;
            var startRotation = _player.transform.rotation;
            var targetRotation = Quaternion.LookRotation (transform.position - _player.transform.position);
            for (var timePassed = 0.0f; timePassed < duration; timePassed += Time.deltaTime) 
            {
                var factor = timePassed / duration;
                _player.transform.rotation = Quaternion.Slerp (startRotation, targetRotation, (float) factor);
                yield return null;
            }
            _player.transform.rotation = targetRotation;
            var position = transform.position;
            var point = new Vector3(position.x, position.y, position.z - 1.0f);
            var obj = Instantiate(FlashEffect, point, Quaternion.identity);
            var elapsedTime = 0.0f;
            while (elapsedTime < 2.0f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.0f, 1.0f, 1.0f), elapsedTime / 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Destroy(obj);
            animationFinished = true;
            _input.enabled = true;
        }
    }
}
