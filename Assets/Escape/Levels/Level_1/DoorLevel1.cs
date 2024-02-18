using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape.Levels.Level_1
{
    public class DoorLevel1 : MethodListener
    {
        [SerializeField] string classValueListener;
        [SerializeField] private Animator DoorAnimator;
        [SerializeField] private Animator LockAnimator;
        private static readonly int LockOpen = Animator.StringToHash("LockOpen");
        private static readonly int DoorOpen = Animator.StringToHash("DoorOpen");
        [SerializeField] private GameObject LockDoor;

        public override bool Method(List<(string, string)> objectValue)
        {
            if (className != classValueListener)
            {
                // TODO: Qui integrerei con la stessa canvas dei suggerimenti che sta facendo @cicciogiac per dire all'utente che ha sbagliato oggetto 
                Debug.Log("Oggetto sbagliato");
                return false;
            }

            ApplyMethod();
            return true;
        }
        public override void ApplyMethod()
        {
            LockDoor.tag = "Untagged";
            LockDoor.GetComponent<Outline>().enabled = false;
            LockAnimator.enabled = true;
            LockAnimator.SetBool(LockOpen, true);
            GetComponent<AudioSource>().Play();
            IEnumerator WaitForLockToBeOpenedAndOpenDoor()
            {
                yield return new WaitUntil(() => LockAnimator.GetCurrentAnimatorStateInfo(0).IsName("LockOpening"));
                yield return new WaitUntil(() => LockAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                LockAnimator.enabled = false;
                DoorAnimator.enabled = true;
                DoorAnimator.SetBool(DoorOpen, true);
                IEnumerator WaitForDoorToBeOpened()
                {
                    yield return new WaitUntil(() => DoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpening"));
                    yield return new WaitUntil(() => DoorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                    DoorAnimator.enabled = false;
                }
                StartCoroutine(WaitForDoorToBeOpened());
            }
            StartCoroutine(WaitForLockToBeOpenedAndOpenDoor());
            DatiPersistenti.istanza.methodsListeners.Add(methodListenerID);
        }
    }
}
