/*
Replace size search with regex and error checking!
*/

using UnityEngine;
using System.Collections;

public class planetsInitialiser : MonoBehaviour {
	public float initSpeed = 5;
	public float orbitRadius = 96;
	GameObject sun;
	Vector3 posDiff;

	// Use this for initialization
	void Start () {
		// get pos from Sun and resize
		sun = GameObject.Find ("sun");
		posDiff = (sun.transform.position - transform.position);
		posDiff.z = 0;

		// resize the posDiff to snap to the radius
		posDiff = posDiff.normalized * (orbitRadius / 100 / 2);

		// setup physics
		HingeSetup ();
		InitVelocity ();
		ColliderSetup ();
		IgnoreCollisions ();
	}

	private void HingeSetup () {
		HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
		Vector3 scale = transform.localScale;

		// Edit the hinge
		hinge.anchor = new Vector2(posDiff.x / (1*scale.x), posDiff.y / (1*scale.x));
		hinge.connectedBody = sun.rigidbody2D;
	}

	private void InitVelocity () {
		Vector3 initDirection;
		initDirection = posDiff.normalized;
		initDirection = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0)) * initDirection;

		rigidbody2D.velocity = initDirection * initSpeed;
	}

	private void ColliderSetup () {
		CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
		SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();

		string sprName = sprRen.sprite.name.ToString ();
		float sprRad = System.Convert.ToSingle( sprName.Split ('_') [2]);

		collider.radius = sprRad / 100 / 2;
	}

	private void IgnoreCollisions () {
		GameObject[] planets = GameObject.FindGameObjectsWithTag ("Planet");

		// ignore all the planets
		foreach (GameObject planet in planets) {
			Physics2D.IgnoreCollision(planet.collider2D, collider2D);
		}
		GameObject satillite = GameObject.Find ("satellite");
		Physics2D.IgnoreCollision(satillite.collider2D, collider2D);
	}
}