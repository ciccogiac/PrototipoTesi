using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastKnownPositionPlayer : MonoBehaviour
{
    public static Vector3 LastKnownPosition;
    public static Quaternion LastKnownRotation;
    [SerializeField] private Transform PlayerTransform;

    // Update is called once per frame
    void Update()
    {
        LastKnownPosition = PlayerTransform.position;
        LastKnownRotation = PlayerTransform.rotation;
    }
}
