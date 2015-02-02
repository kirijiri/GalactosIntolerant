using UnityEngine;
using System.Collections;

public class gravityBeam : MonoBehaviour {
	public bool affected_by_beam = false;
	public bool is_aligned = false;
	bool gravityBeamOn = false;

	// objects
	GameObject sun;
	GameObject ship;
	debug debugScript;
	GameObject planetGraphic;

	Vector3 beamVec;
	Ray beam;
	float beamDist;
	float beamThreshold;
	float alignmentThreshold;
	Vector3 beamIntersectPoint;

	SpriteRenderer spriteRenderer;
	
	void Start () {
		sun = GameObject.Find ("sun");
		ship = GameObject.Find("ship");
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof (debug));
		beamThreshold = ship.GetComponent<shipControl>().gravityBeamThreshold;
		alignmentThreshold = ship.GetComponent<shipControl>().alignmentThreshold;
	}

	// Update is called once per frame
	void Update () {
		// Look for gravity beam
		gravityBeamOn = ship.GetComponent<shipControl>().gravityBeamEngaged;

		// get beam 
		beamVec = (sun.transform.position - ship.transform.position);
		beamVec.z = 0;
		beam = new Ray(ship.transform.position, beamVec*2);

		if (debugScript.drawShipDiameter){
			Debug.DrawRay(ship.transform.position, beamVec*2, Color.white, 3.0f, false);
		}

		// get intersection point and distance to the beam ray
		beamDist = DistanceToRay(beam, transform.position);
		beamIntersectPoint = IntersectionWithRay(beam, transform.position);
		//Debug.DrawLine(transform.position, beamIntersectPoint, Color.red, 3.0f, false);

		// colour the planets when in the threshold and trigger pull
		planetGraphic = GetComponent<planetsInitialiser>().planetGraphic;
		spriteRenderer = planetGraphic.GetComponent<SpriteRenderer>();
		if (beamDist < beamThreshold && gravityBeamOn){
			spriteRenderer.color = Color.red;
			gravityPull(beam, beamIntersectPoint);
			affected_by_beam = true;

			if (beamDist < alignmentThreshold){
				spriteRenderer.color = Color.blue;
				is_aligned = true;
			}
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
