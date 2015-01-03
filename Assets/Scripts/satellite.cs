using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class satellite : MonoBehaviour {
	Vector3 newVec;
	Vector3 newPos;
	Vector3 curPos;
	Vector3 centre;
	public float orbitRadius;
		
	void Start () {
		GameObject sun = GameObject.Find ("sun");
		centre = sun.transform.position;
	}

	void OnMouseDrag () {
		curPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
		curPos.z = 0;
		newVec = curPos - centre;
		newPos = newVec.normalized * (orbitRadius / 100 / 2);
		transform.position = newPos + centre;
	}
}
