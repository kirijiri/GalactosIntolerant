using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    Animator anim;
    Animator thrustAnim;
    int currentState = 0;
    // animation states
    int DEFAULT = 0;
    int PULSING = 1;
    int BEAMBEGIN = 2;
    int BEAMING = 3;

	/*
	 * COMMENTED OUT ANIM STUFF FOR NOW
	 * 
	 * WILL REPLACE WHEN NEW CONTROLLER IS SET UP
	 * 
	 */



    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void SAnimDefault()
    {
        if (currentState != DEFAULT)
        {
            //anim.SetInteger("state", 0);
            currentState = DEFAULT;
        }
    }

    public void SAnimHeld()
    {
        if (currentState != PULSING)
        {
            //anim.SetInteger("state", PULSING);
            currentState = PULSING;
        }
    }

    public void SAnimBeamBegin()
    {
        if (currentState != BEAMBEGIN)
        {
            //anim.SetInteger("state", BEAMBEGIN);
            currentState = BEAMBEGIN;
        }
    }

    public void SAnimBeamOn()
    {
        if (currentState != BEAMING)
        {
            //anim.SetInteger("state", 2);
            GetComponent<SpriteRenderer>().color = Color.red;
            currentState = BEAMING;
        }
    }
}
