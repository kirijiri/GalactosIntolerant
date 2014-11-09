using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {
	private Vector3 dragOrigin;
	private Vector3 dragOriginVelocity;
	private debug debugScript;

	void Start () {
		debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
	}

	void OnMouseDown () {
		dragOrigin = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		dragOriginVelocity = gameObject.rigidbody2D.velocity;
		Debug.Log ("dragOriginVelocity: " + dragOriginVelocity);

		//gameObject.rigidbody2D.isKinematic = true;
	}

	void OnMouseUp () {
		/*
		if (gameObject.rigidbody2D.isKinematic) {
			gameObject.rigidbody2D.isKinematic = false;
			gameObject.rigidbody2D.velocity = dragOriginVelocity;
		}
		*/
	}
	
	void OnMouseDrag () {
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

		dragOrigin.z = 0;
		curPosition.z = 0;
		Vector3 direction = curPosition - dragOrigin;

		// debug print/draw
		if (debugScript.showTouch){
			float intensity = direction.magnitude;
			Debug.Log ("OnMouseDrag " + dragOrigin + " " + curPosition + " dir: " + direction + " intensity:" + intensity);
			Debug.DrawLine (dragOrigin, curPosition, Color.white, 3.0f, false);
		}

		// add mouse drag as force to planet
		//gameObject.rigidbody2D.isKinematic = false;
		rigidbody2D.AddForce(new Vector2(direction.x*100, direction.y*100), ForceMode2D.Force);
	}
}
