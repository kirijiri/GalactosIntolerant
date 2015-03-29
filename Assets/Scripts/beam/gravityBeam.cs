﻿using UnityEngine;
using System.Collections;

public class gravityBeam : MonoBehaviour
{
    public bool isActive = false; // set from ship control
    public bool available = true;

    private GameObject[] planets;
    private gravityBeamAnimation animationCtrl;
    private shipAnimation shipAnim;
    private backgroundSound backgroundAudio;
    private planetSound planetAudio;
    private float timer = 0;

    // settings objects
    private tinker tinker;

    //-------------------------------------------------------------------

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        animationCtrl = this.GetComponent<gravityBeamAnimation>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        shipAnim = GameObject.Find("ship").GetComponent<shipAnimation>();
        backgroundAudio = GameObject.Find("Main Camera").GetComponent<backgroundSound>();
        planetAudio = GameObject.Find("planets").GetComponent<planetSound>();
    }

    void Update()
    {
        if (isActive)
        {
            animationCtrl.BeamAnimationOn();
            if (animationCtrl.IsFinished())
            {
                isActive = false;
                available = false;
                shipAnim.Close();
                backgroundAudio.AudioToDamageSounds();
                planetAudio.AudioToDamageSounds();
            }
        }
    }
    
    public int GetAlignedPlanetCount()
    {
        int alignedPlanetCount = 0;
        
        for (int i = 0; i < planets.Length; i++)
        {
            // count how many planets are aligned
            planetGravityPull beam = planets [i].GetComponent<planetGravityPull>();

            if (beam.isAligned)
                alignedPlanetCount++;
        }
        return alignedPlanetCount;
    }
}
