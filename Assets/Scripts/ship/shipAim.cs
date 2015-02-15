using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class shipAim : MonoBehaviour {
	public float orbitRadius;
	Vector3 sunPos;
		
	void Start () {
		GameObject sun = GameObject.Find ("sun");
		sunPos = sun.transform.position;
	}

	void Update(){
		// lootat sun
		Vector3 dir = sunPos - transform.position; 
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion rot = Quaternion.AngleAxis(angle+90, Vector3.forward);
		transform.rotation = rot;
	}
}
