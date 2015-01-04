using UnityEngine;
using System.Collections;

public class shipOrbitLargeMsg : MonoBehaviour {
	GameObject shipOrbit; 


	// Use this for initialization
	void Start () {
		shipOrbit = GameObject.Find("shipOrbit");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		print ("large hit");
		shipOrbit.SendMessage ("LargeColHit");
	}
}
