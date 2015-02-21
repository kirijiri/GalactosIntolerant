using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int DEFAULT = 0;
    int PULSING = 1;
    int PULSEALONG = 2;
    int BEAMON = 3;
    int currentState = 0;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        //print(currentState);
    }

    public void AnimDefault()
    {
        if (currentState != DEFAULT)
        {
            GetComponent<Animator>().SetInteger("state", DEFAULT);
            currentState = DEFAULT;
        }
    }

    public void AnimHeld()
    {
        if (currentState != PULSING)
        {
            GetComponent<Animator>().SetInteger("state", PULSING);
            currentState = PULSING;
        }
    }

    public void AnimBeamBegin()
    {
        if (currentState != PULSEALONG)
        {
            GetComponent<Animator>().SetInteger("state", PULSEALONG);
            currentState = PULSEALONG;
        }
    }

    public void AnimBeamOn()
    {
        if (currentState != BEAMON)
        {
            GetComponent<Animator>().SetInteger("state", 2);
            GetComponent<SpriteRenderer>().color = Color.red;
            currentState = BEAMON;
        }
    }

    public void AnimClockwise()
    {
    }

    public void AnimAntiClockwise()
    {
    }
}
