using UnityEngine;
using System.Collections;

public class thrusterAnimation : MonoBehaviour
{
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
        anim = this.GetComponent<Animator>();
    }

    public void Hello()
    {
        print("hello");
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
            currentState = BEAMING;
        }
    }
        
    public void AnimClockwise()
    {
        if (currentState != CLOCKWISE)
        {
            print("clock");
            anim.SetInteger("state", CLOCKWISE);
            currentState = CLOCKWISE;
        }
    }
        
    public void AnimAntiClockwise()
    {
        if (currentState != ANTICLOCKWISE)
        {
            print("anticlock");
            anim.SetInteger("state", ANTICLOCKWISE);
            currentState = ANTICLOCKWISE;
        }
    }
}
