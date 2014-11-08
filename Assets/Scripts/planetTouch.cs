using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) 
		{
			Debug.Log ("Pressed left click.");
			// hold -> stop planet

			// move -> accellerate

			// drag to other -> swap
		}
	}
}
