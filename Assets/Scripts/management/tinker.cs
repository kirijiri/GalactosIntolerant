﻿using UnityEngine;
using System.Collections;

// This class stores modifiers for other classes
// to easier tinker around with the mechanics

public class tinker : MonoBehaviour
{
    // planets
    public bool PSlowDownMovementOption = false;
    public float PSlowDownMovementDampingFactor = 0.98f;

    // ship
    public float shipAcceleration = 10f;

    // gravity beam
    public float GBActivateAngle = 45f;
    public float GBEffectThreshold = 0.3f;
    public float GBAlignmentThreshold = 0.05f;
}
