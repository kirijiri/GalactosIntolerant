using UnityEngine;
using System.Collections;

public class planetControl : MonoBehaviour {
	public Vector3 initialVelocity;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = initialVelocity;
	}
}