using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

// This class is dealing with the snapshot button behaviour.
// It 

public class snapshot : MonoBehaviour
{
    private scoreManager scoreManager;
    private gravityBeam gravityBeam;

    //-------------------------------------------------------------------

    void Start()
    {
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
        scoreManager = GameObject.Find("scoreManager").GetComponent<scoreManager>();
        
        // make button invisible (gravity beam will make it visible)
        renderer.enabled = false;
    }

    void OnMouseDown()
    {
        if (!renderer.enabled)
            return;

        // only if gravity beam is on, otherwise ignore
        if (!gravityBeam.enabled) 
            return;

        // save stats
        scoreManager.alignedPlanetCount = gravityBeam.GetAlignedPlanetCount();

        // switch screens
        sceneManager.GoToScoreScreen();
    }
}
