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

    //-------------------------------------------------------------------

    void Start()
    {
        // get UI elements
        planetAlignText = GameObject.Find("planetAligned").GetComponent<Text>();
    }

    void Update()
    {
        print ("HERE: " + gameManager.Instance) ;
        planetAlignText.text = "You aligned " + gameManager.Instance.alignedPlanetCount + " planets";
    }
}
