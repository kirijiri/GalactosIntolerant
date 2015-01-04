using UnityEngine;
using System;
using System.Collections;

public class connectImageToControl : MonoBehaviour {
	public GameObject parent;

	// Update is called once per frame
	void Update () {
		transform.position = parent.transform.position;
	}
}
