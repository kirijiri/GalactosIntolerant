using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {

	//------------------------------------------------------------------- init
	//data
	private Vector3 storedPosition;
	private Vector3 storedVelocity;
	private Vector3 newVelocity;
	// controls
	private bool held = false;
	private bool flicked = false;
	// gameobjects
	private GameObject shipOrbit;
	
	//------------------------------------------------------------------- event functions

	void Start(){
		shipOrbit = GameObject.Find ("shipOrbit");
	}

	void OnMouseDown () {
		// store data
		storedPosition = transform.position;
		storedVelocity = rigidbody2D.velocity;

		// pause
		rigidbody2D.velocity = new Vector3 (0, 0, 0);

		// tell ship orbit to not respond
		shipOrbit.SendMessage ("SetIsOn", false);
	}
	
	void OnMouseDrag () {
		float dist = Vector3.Distance (storedPosition, InputPosition());
		float radius = GetComponent<CircleCollider2D> ().radius;

		if (dist < radius) { 
			held = true;
		} 
		else {
			flicked = true;
			newVelocity = InputPosition () - storedPosition;
		}
		// tell ship orbit to not respond
		shipOrbit.SendMessage ("SetIsOn", false);
	}

	void OnMouseUp(){
		// turn the ship Obit on again
		shipOrbit.SendMessage ("SetIsOn", true);
	}

	void Update () {
		// flicking control
		if (flicked && InputReleased()) {
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
		Vector3 camPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		camPosition.z = 0;
		return camPosition;
	}

	private bool InputReleased() { 
		return Input.GetMouseButtonUp (0); 
	}

	private void ResetControlFlags() { 
		held = false; 
		flicked = false; 
	}
}
