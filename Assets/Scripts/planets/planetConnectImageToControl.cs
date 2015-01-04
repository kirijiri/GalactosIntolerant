using UnityEngine;
using System.Collections;

public class planetConnectImageToControl : MonoBehaviour {
	public GameObject parent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = parent.transform.position;
	}
}
