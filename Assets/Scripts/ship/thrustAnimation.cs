using UnityEngine;
using System.Collections;

public class thrustAnimation : MonoBehaviour {
    Animator anim;
    int currentState = 0;
    // animation states
    int DEFAULT = 0;
    int CLOCKWISE_HIGH = 1;
    int ANTICLOCKWISE_HIGH = 2;
    
    
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    
    public void TAnimDefault()
    {
        if (currentState != DEFAULT)
        {
            anim.SetInteger("state", 0);
            currentState = DEFAULT;
        }
    }
    
    public void TAnimClockWiseHigh()
    {
        if (currentState != CLOCKWISE_HIGH)
        {
            anim.SetInteger("state", CLOCKWISE_HIGH);
            currentState = CLOCKWISE_HIGH;
        }
    }
    
    public void TAnimAntiClockWiseHigh()
    {
        if (currentState != ANTICLOCKWISE_HIGH)
        {
            anim.SetInteger("state", ANTICLOCKWISE_HIGH);
            currentState = ANTICLOCKWISE_HIGH;
        }
    }   
}
