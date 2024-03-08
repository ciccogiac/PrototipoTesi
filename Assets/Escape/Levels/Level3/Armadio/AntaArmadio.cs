using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level3.Armadio
{
    [RequireComponent(typeof(AudioSource))]
    public class AntaArmadio : MethodListener
    {
        [SerializeField] private GameObject Anta;
        [SerializeField] private int DestinationYRotation;
        [SerializeField] private AudioClip OpeningSound;
        [SerializeField] private AudioClip ClosingSound;
        [SerializeField] private AudioSource AudioSource;
        private bool _currentlyOpen;
        private bool _comingFromHere;
        private void PlayAudioClip(AudioClip clip)
        {
            if (AudioSource != null)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }
        public override void Getter(List<(string, string)> objectValue)
        {
            if (_currentlyOpen)
            {
                PlayAudioClip(ClosingSound);
                DatiPersistenti.istanza.methodsListeners.Remove(methodListenerID);
                StartCoroutine(AnimateAnte(false));
            }
        }
        public override bool Method(List<(string, string)> objectValue)
        {
            _comingFromHere = true;
            ApplyMethod();
            return true;
        }
        public override void ApplyMethod()
        {
            if (!_currentlyOpen)
            {
                if (!DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID) || _comingFromHere)
                {
                    PlayAudioClip(OpeningSound);
                    DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
                    _comingFromHere = false;
                }

                StartCoroutine(AnimateAnte(true));
            }
        }
        private IEnumerator AnimateAnte(bool open)
        {
            var startingRotation = Anta.transform.localRotation;
            var correctDestination = open ? DestinationYRotation : 0;
            var targetRotation = Quaternion.Euler(0, correctDestination, 0);
            var timeElapsed = 0.0f;
            while (timeElapsed < 1f)
            {
                Anta.transform.localRotation = Quaternion.Slerp(startingRotation, targetRotation, timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            Anta.transform.localRotation = targetRotation;
            _currentlyOpen = open;
        }
    }
}
