using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class gravityBeam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		Debug.Log ("OnMouseDown");
	}
}
