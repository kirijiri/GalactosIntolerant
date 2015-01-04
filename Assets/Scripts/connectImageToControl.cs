using UnityEngine;
using System;
using System.Collections;

public class connectImageToControl : MonoBehaviour {
	public GameObject parent;
	public GameObject lookat;
	public bool smooth = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// lock to parent position
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

		// look at object
		if (lookat != null) {
			Vector3 dir = lookat.transform.position - transform.position; 
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rot = Quaternion.AngleAxis(angle+90, Vector3.forward);
			transform.rotation = rot;
		}


	}
}
