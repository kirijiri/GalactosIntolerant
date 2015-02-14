using UnityEngine;
using System;
using System.Collections;

public class shipOrbitControl : MonoBehaviour {
	public float sizeLower = 20;
	public float sizeUpper = 80;

	GameObject ship;
	GameObject sun;

	float clickedRadius;
	float orbitRadius;
	float upper;
	float lower;
	float moveToDegrees;

	Vector3 centre;
	Vector3 curPos;
	Vector3 newVec;
    Vector3 solarClick;

	bool isOn = true;
	
	void Start () {
		sun = GameObject.Find ("sun");
		ship = GameObject.Find ("ship");
		centre = sun.transform.position;

		// set orbit size
		SetOrbitRadius ();
		upper = orbitRadius + sizeUpper;
		lower = orbitRadius - sizeLower;
	}

	void Update(){
		// emulating an 'OnMouseUp' behaviour
		if (isOn && Input.GetMouseButton (0)) {
			SetClickedRadius ();
			if (clickedRadius >= lower && clickedRadius <= upper) {
                solarClick = GetSolarPosition();
                ship.SendMessage("SetNewPosition", solarClick);
			}
		}
	}

    Vector3 GetSolarPosition(){
        curPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
        curPos.z = 0;
        return curPos - sun.transform.position;
    }

	void SetClickedRadius(){
		curPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
		curPos.z = 0;
		newVec = curPos - centre;
		clickedRadius = newVec.magnitude * 200; // based on the pixel to unit scale our sprites are using
	}

	void SetOrbitRadius(){
		SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();
		string sprName = sprRen.sprite.name.ToString ();
		orbitRadius = System.Convert.ToSingle( sprName.Split ('_') [2]);
	}

	void SetIsOn(bool setIsOn){
		isOn = setIsOn;
	}
}