using UnityEngine;
using System.Collections;

public class shipOrbitSmallMsg : MonoBehaviour {
	GameObject shipOrbit; 

	// Use this for initialization
	void Start () {
		shipOrbit = GameObject.Find("shipOrbit");
	}

	void OnMouseDown(){
		print ("small hit");
		shipOrbit.SendMessage ("SmallColHit");
	}
}