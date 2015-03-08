using UnityEngine;
using System.Collections;

public class phoneAnimation : MonoBehaviour {

    private Rect restingPosition;

	// Use this for initialization
	void Start () {
        restingPosition = new Rect(80, 140, 235, 50);
    }
    
    void OnGUI()
    {
        Texture2D tx2DFlash = new Texture2D(1, 1); //Creates 2D texture
        tx2DFlash.SetPixel(1, 1, Color.white); //Sets the 1 pixel to be white
        tx2DFlash.Apply(); //Applies all the changes made
        print (tx2DFlash);
        GUI.DrawTexture(restingPosition, tx2DFlash); //Draws the texture for the entire screen (width, height)
	}
}
