using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {
	//data
	private Vector3 storedPosition;
	private Vector3 storedVelocity;
	// debug
	private debug debugScript;
	// controls
	private bool holding = false;
	private bool flicking = false;
	private bool held = false;
	private bool fluck = false;

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
		held = true;
	}

	void Update () {
		// holding control
		if (held && InputReleased()) {
			Debug.Log(storedVelocity);
			rigidbody2D.velocity = storedVelocity;

			// reset 
			held = false;
			return;
		}
	}

	private Vector3 InputPosition () {
		return Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));}
	private bool InputReleased(){
		return Input.GetMouseButtonUp (0);}
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