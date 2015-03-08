using UnityEngine;
using System.Collections;

// This class stores modifiers for other classes
// to easier tinker around with the mechanics

public class tinker : MonoBehaviour
{
    // planets
	public string planetOptions = "--- PLANET OPTIONS ---";
    public bool printPlanetControls = false;
    public float PInitSpeedMultiplier = 1f;
    public bool PSlowDownMovementOption = true;
    public float PInnerBand = 0f;
    public float POuterBand = 200f;
    public bool PFlickOnOuterBand = false;
    public float PMaxSecsForDrag = 3f;
    public float PMaxSecsForHold = 3f;
    public float PForceMult = 0.02f;
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

    // messages
    public string messageOptions = "--- MESSAGE OPTIONS ---";
    public float idleTimer = 1.0f;//7.0f;
    public float messageX = 0.0f;//80.0f;
    public float messageY = 0.0f;//140.0f;
    public float messageWidth = 235.0f;
    public float messageHeight = 20.0f;
    public float messageVSpace = 20.0f;
    public float[] messageMargins = new float[4]{10.0f, 10.0f, 10.0f, 10.0f}; //left, top, right, bottom
    public float characterWidth = 10.0f;
    public float phoneHeight = 380.0f;
}
