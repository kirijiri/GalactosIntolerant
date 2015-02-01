/*
Replace size search with regex and error checking!
*/

using UnityEngine;
using System.Collections;

public class planetsInitialiser : MonoBehaviour {
	public GameObject planetGraphic;
	float initSpeed;
	float orbitRadius;
	GameObject sun;
	Vector3 posDiff;
	planetStats planetStats;

	// Use this for initialization
	void Start () {

		// get stats from stat class
		initSpeed = GetComponent<planetStats>().initSpeed;
		orbitRadius = GetComponent<planetStats>().orbitRadius;

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
		MakeImagePrefab ();
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
		SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();
		CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
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
		GameObject satillite = GameObject.Find ("ship");
		Physics2D.IgnoreCollision(satillite.collider2D, collider2D);
	}

	private void MakeImagePrefab(){
		SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();
		GameObject imagePrefab = Instantiate (Resources.Load ("image_prefab")) as GameObject;
		
		// setup a image prefab, then turn off sprite
		imagePrefab.name = gameObject.name + "_IMAGE";
		imagePrefab.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
		imagePrefab.GetComponent<connectImageToControl> ().parent = gameObject;
		sprRen.enabled = false;

		planetGraphic = imagePrefab;
	}
}