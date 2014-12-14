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

	private void HingeSetup(){
		HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
		Vector3 scale = transform.localScale;
		
		// Edit the hinge
		hinge.anchor = new Vector2(posDiff.x / (1*scale.x), posDiff.y / (1*scale.x));
		hinge.connectedBody = sun.rigidbody2D;
	}

	private void InitVelocity(){
		Vector3 initDirection;
		initDirection = posDiff.normalized;
		initDirection = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0)) * initDirection;

		rigidbody2D.velocity = initDirection * initSpeed;
	}
}