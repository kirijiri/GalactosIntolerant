using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	// globals
	public int aligned_planet_count = 0;

	void Awake () {
		Debug.Log ("new gameManager");
		DontDestroyOnLoad(this);
	}
}
