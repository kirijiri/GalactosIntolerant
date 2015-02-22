using UnityEngine;
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

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    public void SAnimDefault()
    {
        if (currentState != DEFAULT)
        {
            anim.SetInteger("state", 0);
            currentState = DEFAULT;
        }
    }

    public void SAnimHeld()
    {
        if (currentState != PULSING)
        {
            anim.SetInteger("state", PULSING);
            currentState = PULSING;
        }
    }

    public void SAnimBeamBegin()
    {
        if (currentState != BEAMBEGIN)
        {
            anim.SetInteger("state", BEAMBEGIN);
            currentState = BEAMBEGIN;
        }
    }

    public void SAnimBeamOn()
    {
        if (currentState != BEAMING)
        {
            anim.SetInteger("state", 2); //<-- just reusing pulsealong for now
            GetComponent<SpriteRenderer>().color = Color.red;
            currentState = BEAMING;
        }
    }
}
