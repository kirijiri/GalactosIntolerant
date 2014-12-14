using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class satellite : MonoBehaviour {
	Vector3 newVec;
	Vector3 newPos;
	Vector3 curPos;
	Vector3 centre;
	float radius;
		
	void Start () {
		GameObject sun = GameObject.Find ("sun");
		centre = sun.transform.position;

		Vector3 diff = (sun.transform.position - transform.position);
		radius = diff.magnitude;
	}

	void OnMouseDrag () {
		curPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
		curPos.z = 0;
		newVec = curPos - centre;
		newPos = newVec.normalized * radius;
		transform.position = newPos + centre;
	}
}
