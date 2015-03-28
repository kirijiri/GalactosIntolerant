using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This class is for displaying the scores
// in the score screen

public class score : MonoBehaviour
{
    public int alignedPlanetCount;

    // flash animation
    private bool doSnapshot = true;
    private float fadeSpeed;
    
    // UI elements
    private Text planetAlignText;
    private tinker tinker;

    //-------------------------------------------------------------------

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        // get UI elements

    }

    void Update()
    {
        // tinker update
        fadeSpeed = tinker.flashFadeOutSpeed;

        // show score
        planetAlignText.text = "You aligned " + gameManager.Instance.alignedPlanetCount + " planets";
    }

    void OnGUI()
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
