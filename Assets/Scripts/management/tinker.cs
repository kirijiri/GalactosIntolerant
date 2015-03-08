using UnityEngine;
using System.Collections;

// This class stores modifiers for other classes
// to easier tinker around with the mechanics

public class tinker : MonoBehaviour
{
    // planets
	public string planetOptions = "--- PLANET OPTIONS ---";
    public bool printPlanetControls = false;
    public float PInitSpeedMultiplier = 1;
    public bool PSlowDownMovementOption = true;
    public float PInnerBand = 0;
    public float POuterBand = 200;
    public float PMaxSecsForDrag = 3;
    public float PMaxSecsForHold = 3;
    public float PForceMult = 0.02;
    public float PAcceleration = 0.25f;

    // option 1
    public float PSlowDownMovementDampingFactor = 0.98f;

    // option 2
    public bool PUseForcesOption = true;
    public float PUseForcesDragAmount = 5.0f;

    // ship
	public string shipOptions = "--- SHIP OPTIONS ---";
    public float shipAcceleration = 5f;
    public float shipDeceleration = 10f;
    public float shipOrbitRadius = 236f;
    public float shipClickUpperBoundary = 80f;
    public float shipClickLowerBoundary = 20f;
    public float shipAnimRestThreshold = 5.0f;

    // gravity beam
	public string beamOptions = "--- BEAM OPTIONS ---";
    public float GBActivateAngle = 45f;
    public float GBEffectThreshold = 0.3f;
    public float GBAlignmentThreshold = 0.05f;
    public float GBTimeout = 3.0f;
}
