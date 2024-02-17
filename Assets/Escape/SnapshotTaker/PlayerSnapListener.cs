using System;
using UnityEngine;

namespace Escape.SnapshotTaker
{
    public class PlayerSnapListener : MonoBehaviour
    {
        [SerializeField] private SnapshotCamera SnapCam;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
                SnapCam.CallTakeSnapshot();
        }
    }
}
