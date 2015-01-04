using UnityEngine;
using System;
using System.Collections;

public class planetConnectImageToControl : MonoBehaviour {
	public GameObject parent;
	public bool smooth = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = parent.transform.position;
		if (smooth) 
		{
			transform.position = pos;
		} 
		else 
		{
			// rounding to 2 DP due to pixel ratio in images
			float x = (float)Math.Round (pos.x, 2);
			float y = (float)Math.Round (pos.y, 2);
			transform.position = new Vector3 (x, y, 0);
		}
	}
}
