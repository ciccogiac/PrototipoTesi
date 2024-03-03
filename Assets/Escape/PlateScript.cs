using System;
using System.Collections;
using UnityEngine;

namespace Escape
{
    public class PlateScript : MonoBehaviour
    {
        [SerializeField] private GameObject Printer;
        private Outline _outline;
        private bool _visible = true;
        private Vector3 _startingScale;

        private void Start()
        {
            _startingScale = transform.localScale;
            _outline = GetComponent<Outline>();
        }

        private void Update() {
            if (_visible)
            {
                if (!Printer.activeSelf)
                {
                    transform.localScale = Vector3.zero;
                    _visible = false;
                }
            }

            if (!_visible)
            {
                if (Printer.activeSelf && Printer.transform.localScale != Vector3.zero)
                {
                    StartCoroutine(AnimateScaling());
                    _visible = true;
                }
            }

            var printerOutline = Printer.GetComponent<Outline>();
            _outline.enabled = printerOutline && printerOutline.enabled;
        }

        private IEnumerator AnimateScaling()
        {
            var elapsedTime = 0.0f;
            while (elapsedTime < 2.0f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _startingScale, elapsedTime / 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = _startingScale;
        }
    }
}
