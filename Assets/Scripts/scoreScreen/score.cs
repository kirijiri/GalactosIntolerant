﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This class is for displaying the scores
// in the score screen

public class score : MonoBehaviour
{
    public int maxPlanetCount = 5;
	private double likes;

    // flash animation
    private bool doSnapshot = true;
    private float fadeSpeed;
    
    // UI elements
    private Text planetAlignText;
    private Text likesText;
    private Text followersText;
    private Text populationText;
    private Text deathText;
    private Text childrenText;

    private float likesPercentage;
    private float childrenPercentage;

    private tinker tinker;

    //-------------------------------------------------------------------

	private void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        likesPercentage = tinker.likesPercentage;
        childrenPercentage = tinker.childrenPercentage;

        // get UI elements
        planetAlignText = GameObject.Find("planetAligned").GetComponent<Text>();
        likesText = GameObject.Find("likes").GetComponent<Text>();
        followersText = GameObject.Find("followers").GetComponent<Text>();
        populationText = GameObject.Find("population").GetComponent<Text>();
        deathText = GameObject.Find("death").GetComponent<Text>();
        childrenText = GameObject.Find("children").GetComponent<Text>();
    }

	private void Update()
    {
        // tinker update
        fadeSpeed = tinker.flashFadeOutSpeed;

		// calculate likes
		if (gameManager.Instance.alignedPlanetCount > 0)
			likes = (gameManager.Instance.followers * likesPercentage) / (maxPlanetCount - gameManager.Instance.alignedPlanetCount);
		else
			likes = (gameManager.Instance.followers * likesPercentage) / (maxPlanetCount - gameManager.Instance.alignedPlanetCount);

        // show score
        planetAlignText.text = "You aligned " + gameManager.Instance.alignedPlanetCount + " planets";
		likesText.text =  string.Format("{0:0}", likes); 
        followersText.text = string.Format("{0:0}", gameManager.Instance.followers); 
        populationText.text =  string.Format("{0:0}", gameManager.Instance.population); 
        deathText.text = string.Format("{0:0}", gameManager.Instance.dead); 
        childrenText.text = string.Format("{0:0}", gameManager.Instance.dead * childrenPercentage);
    }

	private string customFormatting (string number)
	{
		print (number);
		return number;
	}

	private void OnGUI()
    {
        if (!doSnapshot)
            return;
        
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

        Texture2D tx;
        tx = new Texture2D(1, 1);          
        Color lerpedColor = Color.Lerp(Color.white, Color.clear, Time.timeSinceLevelLoad * fadeSpeed);
        tx.SetPixel(1, 1, lerpedColor);    
        tx.Apply();                        
        
        GUI.DrawTexture(screenRect, tx); 

        // skip draw routine
        if (Time.timeSinceLevelLoad >= 1) doSnapshot = false;
    }
}
