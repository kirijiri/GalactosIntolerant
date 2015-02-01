using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class phoneMessages : MonoBehaviour {

	GameObject[] planets;

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "hello";


		planets = GameObject.FindGameObjectsWithTag ("Planet");
		for (int i = 0; i < planets.Length; i++) {
			Debug.Log(planets[i].GetComponent<planetStats>().GetIdleMessage());
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
