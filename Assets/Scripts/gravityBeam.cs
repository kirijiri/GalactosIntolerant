using UnityEngine;
using System.Collections;

public class gravityBeam : MonoBehaviour
{
    public bool isActive = false; // set from ship control

    private GameObject[] planets;

    //-------------------------------------------------------------------

    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
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
