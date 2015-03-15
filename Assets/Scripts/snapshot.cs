using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

// This class is dealing with the snapshot button behaviour.

public class snapshot : MonoBehaviour
{
    private gravityBeam gravityBeam;

    //-------------------------------------------------------------------

    void Start()
    {
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
        
        // make button invisible (gravity beam will make it visible)
        renderer.enabled = false;
    }

    void OnMouseDown()
    {
        if (!renderer.enabled)
            return;

        if (!gravityBeam.isActive && gravityBeam.available) 
            return;

        // save stats
        gameManager.Instance.alignedPlanetCount = gravityBeam.GetAlignedPlanetCount();

        // switch screens
        sceneManager.GoToScoreScreen();
    }
}
