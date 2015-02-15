using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This class is for displaying the scores
// in the score screen

public class score : MonoBehaviour
{
    public int alignedPlanetCount;
    
    // UI elements
    private Text planetAlignText;

    // score manager
    scoreManager scoreManager;

    //-------------------------------------------------------------------

    void Start()
    {
        // get score manager
        scoreManager = GameObject.Find("scoreManager").GetComponent<scoreManager>();

        // get UI elements
        planetAlignText = GameObject.Find("planetAligned").GetComponent<Text>();
    }

    void Update()
    {
        planetAlignText.text = "You aligned " + scoreManager.alignedPlanetCount + " planets";
    }
}
