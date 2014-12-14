using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {
	//------------------------------------------------------------------- init
	//data
	private Vector3 storedPosition;
	private Vector3 storedVelocity;
	private Vector3 newVelocity;
	// debug
	private debug debugScript;
	// controls
	private bool held = false;
	private bool flooked = false;

	void Start () {
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
	}
	
	//------------------------------------------------------------------- event functions

	void OnMouseDown () {
		// store data
		storedPosition = transform.position;
		storedVelocity = rigidbody2D.velocity;

		// pause
		rigidbody2D.velocity = new Vector3 (0, 0, 0);
	}
	
	void OnMouseDrag () {
		float dist = Vector3.Distance (storedPosition, InputPosition());
		float radius = GetComponent<CircleCollider2D> ().radius;

		if (dist < radius) { 
			held = true; }
		else{
			flooked = true;
			newVelocity = InputPosition() - storedPosition;
		}
	}

	void Update () {
		// flicking control
		if (flooked && InputReleased()) {
			rigidbody2D.velocity = newVelocity;
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

	//------------------------------------------------------------------- private functions
	private Vector3 InputPosition () {
		Vector3 camPosition =  Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		camPosition.z = 0;
		return camPosition;
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
