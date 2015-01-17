using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {
	int aligned_planet_count = 0;

	// globals
	private gameManager manager;

	// Use this for initialization
	void Start () {
		manager = (gameManager)GameObject.FindObjectOfType(typeof(gameManager));
		aligned_planet_count = manager.aligned_planet_count;
		Debug.Log("Changed room - aligned_planet_count: " + aligned_planet_count);
	}
}
