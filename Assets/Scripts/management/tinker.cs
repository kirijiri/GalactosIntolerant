﻿using UnityEngine;
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
    public float idleTimer = 3.0f;
    public float criticalPopulationPercentage = 30.0f;

    public string messageAnimOptions = "--- MESSAGE ANIMATION OPTIONS ---";
    public float[] messageMargins = new float[4]{3.0f, 3.0f, 3.0f, 3.0f}; //left, top, right, bottom
    public float messageVSpace = 5.0f;
    public float scrollSpeed = 2.0f;
    public float phoneWidth = 98.0f;
    public float phoneHeight = 118.0f;
    public float phoneYOffset = 10.0f;
    public float bgWidth = 480.0f;
    public float bgHeight = 270.0f;
    public int fontSize = 22;

    public string scoreOptions = "--- SCORE OPTIONS ---";
    public float followerX = 15;
    public float followerY = 210;
    public float followerWidth = 100;
    public float followerHeight = 50;
    public int followerFontSize = 30;
    public float populationDiesPercentage = 30.0f;

    public string snapshotOptions = "--- SNAPSHOT OPTIONS ---";
    public float flashFadeInSpeed = 10.0f;
    public float flashFadeOutSpeed = 10.0f;
}
