using UnityEngine;
using System.Collections;

// This class is for keeping track of the score.
// It will not get deleted between scene switches and can be 
// accessed from another level unless manually deleted

public class scoreManager : MonoBehaviour
{
    public int alignedPlanetCount = 0;

    //-------------------------------------------------------------------

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
