﻿using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    Animator thrustAnim;
    int currentState = 0;
    // animation states
    int DEFAULT = 0;
    int PULSING = 1;
    int BEAMBEGIN = 2;
    int BEAMING = 3;
    int CLOCKWISE = 4;
    int ANTICLOCKWISE = 5;


    // Use this for initialization
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        thrustAnim = GameObject.Find("thrusters").GetComponent<Animator>();
        anim = this.GetComponent<Animator>();
    }

    public void AnimDefault()
    {
        if (currentState != DEFAULT)
        {
            anim.SetInteger("state", 0);
            currentState = DEFAULT;
        }
    }

    public void AnimHeld()
    {
        if (currentState != PULSING)
        {
            anim.SetInteger("state", PULSING);
            currentState = PULSING;
        }
    }

    public void AnimBeamBegin()
    {
        if (currentState != BEAMBEGIN)
        {
            anim.SetInteger("state", BEAMBEGIN);
            currentState = BEAMBEGIN;
        }
    }

    public void AnimBeamOn()
    {
        if (currentState != BEAMING)
        {
            anim.SetInteger("state", BEAMING); //<-- just reusing pulsealong for now
            GetComponent<SpriteRenderer>().color = Color.red;
            currentState = BEAMING;
        }
    }

    public void AnimClockwise()
    {
        if (currentState != CLOCKWISE)
        {
            anim.SetInteger("state", CLOCKWISE);
            currentState = CLOCKWISE;
        }
    }

    public void AnimAntiClockwise()
    {
        if (currentState != ANTICLOCKWISE)
        {
            anim.SetInteger("state", ANTICLOCKWISE);
            currentState = ANTICLOCKWISE;
        }
    }
}
