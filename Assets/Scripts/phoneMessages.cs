﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class phoneMessages : MonoBehaviour {

	GameObject[] planets;
	float timer = 0;

	List<int> planetsTrack;

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = "Oh hello";
		planets = GameObject.FindGameObjectsWithTag ("Planet");

		ResetPlanetTrack();
	}

	void ResetPlanetTrack() {
		planetsTrack = new List<int>();
		for (int i = 0; i < planets.Length; i++) planetsTrack.Add(i);
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer > 7) {
			timer = 0;

			// get a random planet index that hasn't been used yet (if it runs out, fill up the list again)
			// TODO: figure out how many messages are in each planet, the planets might not have to change,
			// just the messages
			if (planetsTrack.Count == 0) ResetPlanetTrack ();
			int rand = Random.Range(0, planetsTrack.Count);

			// set idle message text
			GetComponent<Text> ().text = planets [rand].GetComponent<planetStats> ().GetIdleMessage ();

			// remove planet from track list to not repeat it immidiately again
			planetsTrack.RemoveAt(rand);
		}
	}
}
