using UnityEngine;
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

	// custom text formatting
	private string[] formats = new string[4]{"", "k", "m", "b"};
	private int count;

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

		// tinker update
        fadeSpeed = tinker.flashFadeOutSpeed;

		// calculate likes
		if (gameManager.Instance.alignedPlanetCount == maxPlanetCount)
			likes = (gameManager.Instance.followers * likesPercentage);
		else
			likes = (gameManager.Instance.followers * likesPercentage) / (maxPlanetCount - gameManager.Instance.alignedPlanetCount);

		print ("likes: " + likes + " followers:" + gameManager.Instance.followers + " population:" + gameManager.Instance.population + " dead: " + gameManager.Instance.dead + " children: " + childrenPercentage + " likesPercentage" + likesPercentage);

        // show score
        planetAlignText.text = "You aligned " + gameManager.Instance.alignedPlanetCount + " planets";
		likesText.text = customFormatting(likes);
		followersText.text = customFormatting(gameManager.Instance.followers);
		populationText.text =  customFormatting(gameManager.Instance.population);
		deathText.text = customFormatting(gameManager.Instance.dead);
		childrenText.text = customFormatting(gameManager.Instance.dead * childrenPercentage);
    }

	private string customFormatting (double number)
	{
		count = 0;
		number = recursive_formatting(number, ref count);
		if (count > 0) {
			print ("count: " + count + " - length:" + formats.Length + " - number:" + number);
			return (string.Format ("{0:0.00}", number) + "" + formats [count]);
		}
		else
			return (string.Format("{0:0}", number));
	}
			
	private double recursive_formatting (double number, ref int count)
	{
		if ((number/1000 < 0.5) || (count+1 > formats.Length))
		{
			return number;
		}
		count++;
		number = recursive_formatting((number/1000), ref count);
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

