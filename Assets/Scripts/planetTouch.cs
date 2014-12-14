using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {
	//data
	private Vector3 storedPosition;
	private Vector3 storedVelocity;
	private Vector3 offsetVelocity;
	// debug
	private debug debugScript;
	// controls
	private bool held = false;
	private bool flooked = false;

	void Start () {
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
	}

	void OnMouseDown () {
		// store data
		storedPosition = transform.position;
		storedVelocity = rigidbody2D.velocity;

		// pause
		rigidbody2D.velocity = new Vector3 (0, 0, 0);
	}
	
	void OnMouseDrag () {
		float dist;
		dist = Vector3.Distance (storedPosition, InputPosition());

		if (dist < GetComponent<CircleCollider2D>().radius) {
			held = true;
			}
		else{
			flooked = true;
			offsetVelocity = InputPosition() - storedPosition;
		}
	}

	void Update () {
		// flicking control
		if (flooked && InputReleased()) {
			float dot = Vector3.Dot(storedVelocity, offsetVelocity);

			// invert the velocity if we're changing direction
			if (dot > 0){
				rigidbody2D.velocity = storedVelocity + offsetVelocity;
			}
			else {
				rigidbody2D.velocity = (storedVelocity * -1) + offsetVelocity;
			}

			ResetControlFlags();
			return;
		}

		// holding control
		if (held && InputReleased()) {
			rigidbody2D.velocity = storedVelocity;
			ResetControlFlags();
			return;
		}
	}

	private Vector3 InputPosition () {
		return Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
	}

	private bool InputReleased() { 
		return Input.GetMouseButtonUp (0); 
	}

	private void ResetControlFlags() { 
		held=false; 
		flooked=false; 
	}
}


/*
void OnMouseDown () {
	dragOrigin = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
	dragOriginVelocity = gameObject.rigidbody2D.velocity;	
}

void OnMouseDrag () {
	Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
	
	dragOrigin.z = 0;
	curPosition.z = 0;
	Vector3 direction = curPosition - dragOrigin;
	
	// debug print/draw
	if (debugScript.showTouch){
		//float intensity = direction.magnitude;
		//Debug.Log ("OnMouseDrag " + dragOrigin + " " + curPosition + " dir: " + direction + " intensity:" + intensity);
		Debug.DrawLine (dragOrigin, curPosition, Color.white, 3.0f, false);
	}
	
	// add mouse drag as force to planet
	rigidbody2D.AddForce(new Vector2(direction.x*100, direction.y*100), ForceMode2D.Force);
}
*/
