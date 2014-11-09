using UnityEngine;
using System.Collections;

public class planetsInitialiser : MonoBehaviour {
	public float initSpeed;

	// Use this for initialization
	void Start () {
		GameObject sun;
		HingeJoint2D hinge;
		JointMotor2D motor;
		Vector3 posDiff;

		sun = GameObject.Find ("sun");
		hinge = gameObject.GetComponent<HingeJoint2D>();
		motor = hinge.motor;
		posDiff = (sun.transform.position - transform.position);
		
		// Edit the hinge
		hinge.anchor = new Vector2(posDiff.x, posDiff.y);
		hinge.connectedBody = sun.rigidbody2D;
		
		motor.motorSpeed = initSpeed;
		hinge.motor = motor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
