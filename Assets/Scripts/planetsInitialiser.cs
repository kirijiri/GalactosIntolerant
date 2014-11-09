using UnityEngine;
using System.Collections;

public class planetsInitialiser : MonoBehaviour {
	public float initSpeed = 5;
	GameObject sun;
	Vector3 posDiff;

	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("sun");
		posDiff = (sun.transform.position - transform.position);

		HingeSetup ();
		InitVelocity ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HingeSetup(){
		HingeJoint2D hinge;

		hinge = gameObject.GetComponent<HingeJoint2D>();
		
		// Edit the hinge
		hinge.anchor = new Vector2(posDiff.x, posDiff.y);
		hinge.connectedBody = sun.rigidbody2D;
	}

	private void InitVelocity(){
		Vector3 initDirection;

		initDirection = posDiff.normalized;
		initDirection = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0)) * initDirection;

		rigidbody2D.velocity = initDirection * initSpeed;
	}
}
