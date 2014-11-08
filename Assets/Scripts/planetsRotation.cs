using UnityEngine;
using System.Collections;

public class planetsRotation : MonoBehaviour {
	public string myName;

	// Use this for initialization
	void Start () {
		Debug.Log("I am alive and my name is " + myName);
		Debug.Log (transform.position);
		Debug.Log (transform.parent.position);
		transform.position = new Vector3(2,2,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
