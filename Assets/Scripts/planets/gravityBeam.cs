﻿using UnityEngine;
using System.Collections;

public class gravityBeam : MonoBehaviour {

	// objects
	GameObject sun;
	GameObject ship;
	GameObject planetGraphic;

	Vector3 beamVec;
	Ray beam;
	float beamDist;
	Vector3 beamIntersectPoint;
	bool gravityBeamOn = false;

	// DEBUG: threshold should be public and global?
	public float beam_threshold = 0.1f;

	// debug
	private debug debugScript;
	
	void Start () {
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
	}

	// Update is called once per frame
	void Update () {

		// DEBUG: set gravitybeam to debug one
		gravityBeamOn = debugScript.gravityBeamOn;

		// find sun and ship
		sun = GameObject.Find ("sun");
		ship = GameObject.Find ("ship");

		// get beam 
		beamVec = (sun.transform.position - ship.transform.position);
		beamVec.z = 0;
		beam = new Ray(ship.transform.position, beamVec*2);
		Debug.DrawRay(ship.transform.position, beamVec*2, Color.white, 3.0f, false);

		// get intersection point and distance to the beam ray
		beamDist = DistanceToRay(beam, transform.position);
		beamIntersectPoint = IntersectionWithRay(beam, transform.position);
		//Debug.DrawLine(transform.position, beamIntersectPoint, Color.red, 3.0f, false);

		// colour the planets when in the threshold and trigger pull
		planetGraphic = GetComponent<planetsInitialiser>().planetGraphic;
		SpriteRenderer spriteRenderer = planetGraphic.GetComponent<SpriteRenderer>();
		if (beamDist < beam_threshold && gravityBeamOn){
			spriteRenderer.color = Color.red;
			gravityPull(beam, beamIntersectPoint);
		}
		else{
			spriteRenderer.color = Color.white;
		}
	}

	// pull planets to the beam
	private void gravityPull (Ray beam, Vector3 intersect) {
		rigidbody2D.velocity = intersect - transform.position;
	}

	private static float DistanceToRay (Ray ray, Vector3 point) {
		return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
	}
	
	private static Vector3 IntersectionWithRay (Ray ray, Vector3 point) {
		return ray.origin + ray.direction * Vector3.Dot(ray.direction, point - ray.origin);
	}
}
