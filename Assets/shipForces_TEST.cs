using UnityEngine;
using System.Collections;

public class shipForces_TEST : MonoBehaviour {
	GameObject sun;
	Vector3 posDiff;


	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("sun");
		posDiff = (sun.transform.position - transform.position);
		posDiff.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void HingeSetup () {
		HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
		Vector3 scale = transform.localScale;
		
		// Edit the hinge
		hinge.anchor = new Vector2(posDiff.x / (1*scale.x), posDiff.y / (1*scale.x));
		hinge.connectedBody = sun.rigidbody2D;
	}
}
