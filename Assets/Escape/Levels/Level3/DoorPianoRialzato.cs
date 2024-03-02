using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level3
{
    public class DoorPianoRialzato : MethodListener
    {
        [SerializeField] string classValueListener;
        [SerializeField] private Animator DoorAnimator;
        [SerializeField] private Animator LockAnimator;
        private static readonly int LockOpen = Animator.StringToHash("LockOpen");
        private static readonly int DoorOpen = Animator.StringToHash("DoorOpen");
        [SerializeField] private GameObject LockDoor;
        [SerializeField] private int _hintNumber;
        [SerializeField] private LevelHint LevelHint;
        private bool _comingFromHere;
        public override bool Method(List<(string, string)> objectValue)
        {
            if (className != classValueListener)
            {
                // TODO: Qui integrerei con la stessa canvas dei suggerimenti che sta facendo @cicciogiac per dire all'utente che ha sbagliato oggetto 
                Debug.Log("Oggetto sbagliato");
                return false;
            }

            _comingFromHere = true;
            ApplyMethod();
            return true;
        }
        public override void ApplyMethod()
        {
            LockDoor.tag = "Untagged";
            LockDoor.GetComponent<Outline>().enabled = false;
            LockAnimator.enabled = true;
            LockAnimator.SetBool(LockOpen, true);
            if (!DatiPersistenti.istanza.methodsListeners.Contains(methodListenerID) || _comingFromHere)
            {
                GetComponent<AudioSource>().Play();
                DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
                LevelHint.nextHint(_hintNumber);
                _comingFromHere = false;
            }
            IEnumerator WaitForLockToBeOpenedAndOpenDoor()
            {
                yield return new WaitUntil(() => LockAnimator.GetCurrentAnimatorStateInfo(0).IsName("LockOpening"));
                yield return new WaitUntil(() => LockAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                LockAnimator.enabled = false;
                DoorAnimator.enabled = true;
                DoorAnimator.SetBool(DoorOpen, true);
                IEnumerator WaitForDoorToBeOpened()
                {
                    yield return new WaitUntil(() => DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorPianoRialzatoOpening"));
                    yield return new WaitUntil(() => DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                    DoorAnimator.enabled = false;
                }
                StartCoroutine(WaitForDoorToBeOpened());
            }
            StartCoroutine(WaitForLockToBeOpenedAndOpenDoor());
        }
    }
}
