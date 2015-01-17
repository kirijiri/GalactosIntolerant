using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreManager : MonoBehaviour {
	int aligned_planet_count = 0;

	// UI elements
	Text planetAlignText;

	// globals
	private gameManager manager;

	// Use this for initialization
	void Start () {

		// get stats
		manager = (gameManager)GameObject.FindObjectOfType(typeof(gameManager));
		aligned_planet_count = manager.aligned_planet_count;

		// get UI elements
		planetAlignText = GameObject.Find("planetAligned").GetComponent<Text> ();
	}

	void Update() {
		planetAlignText.text = "You aligned " + aligned_planet_count + " planets";
	}
}
