using UnityEngine;
using System.Collections;

// This class stores modifiers for other classes
// to easier tinker around with the mechanics

public class tinker : MonoBehaviour
{
    // planets
    public bool PSlowDownMovementOption = false;
    public float PSlowDownMovementDampingFactor = 0.98f;

    // ship
    public float shipAcceleration = 5f;
    public float shipDeceleration = 10f;
    public float shipOrbitRadius = 236f;
    public float shipClickUpperBoundary = 80f;
    public float shipClickLowerBoundary = 20f;

    // gravity beam
    public float GBActivateAngle = 45f;
    public float GBEffectThreshold = 0.3f;
    public float GBAlignmentThreshold = 0.05f;
}
