using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class snapshot : MonoBehaviour {
	GameObject[] planets;
	GameObject ship;
	gravityBeam beam_script;
	int aligned_planet_count = 0;

	// globals
	private gameManager manager;

	// Use this for initialization
	void Start () {
		ship = GameObject.Find("ship");
		renderer.enabled = false;
	}

	void OnMouseDown () {
		if (!renderer.enabled) return;

		// only if gravity beam is on, otherwise ignore
		if (!ship.GetComponent<shipControl>().gravityBeamEngaged) 
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

		// save stats
		manager = (gameManager)GameObject.FindObjectOfType(typeof(gameManager));
		manager.aligned_planet_count = aligned_planet_count;

		// move rooms
		Application.LoadLevel ("scoreScreen");
	}
}
