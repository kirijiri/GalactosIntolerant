using UnityEngine;
using System.Collections;

public class shipControl : MonoBehaviour {
	public float maxSpeed = 2;
	float orbitRadius;
	float curDegree;
	float tarDegree;
	Vector3 sunPos;
	Vector3 systemPos;
	Vector3 setDir;

	void Start(){
		GameObject sun = GameObject.Find ("sun");
		sunPos = sun.transform.position;
		orbitRadius = gameObject.GetComponent<shipInitialiser>().orbitRadius;
	}

	float GetCurDegrees(){
		systemPos = gameObject.transform.position - sunPos;
		return Mathf.Atan2 (systemPos.y, systemPos.x) * Mathf.Rad2Deg;
	}

	void SetToDegree(float degree){
		setDir = Quaternion.AngleAxis (degree, Vector3.forward) * Vector3.right;
		transform.position = sunPos + (setDir.normalized * (orbitRadius / 100 / 2));
	}
	
	void MoveTo(float degree){
		SetToDegree( degree );
	}

	void Update(){

	}

}
