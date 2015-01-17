using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class snapshot : MonoBehaviour {
	GameObject[] planets;
	gravityBeam beam_script;
	int aligned_planet_count = 0;

	// debug
	private debug debugScript;

	// Use this for initialization
	void Start () {
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
	}

	void OnMouseDown () {
		// only if gravity beam is on, otherwise ignore
		if (!debugScript.gravityBeamOn) 
			return;

		aligned_planet_count = 0;

		planets = GameObject.FindGameObjectsWithTag ("Planet");
		for (int i = 0; i < planets.Length; i++) {
			// freeze planets
			planets[i].GetComponent<Rigidbody2D>().isKinematic = true;

			// count how many planets are aligned
			beam_script = (gravityBeam)planets[i].GetComponent(typeof(gravityBeam));
			if (beam_script.affected_by_beam)
				aligned_planet_count++;
		}

		// move rooms
		Debug.Log("Change room - aligned_planet_count: " + aligned_planet_count);
		Application.LoadLevel ("scoreScreen");
	}
}
