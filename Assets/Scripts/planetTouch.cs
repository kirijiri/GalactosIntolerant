using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]

public class planetTouch : MonoBehaviour {
	private Vector3 dragOrigin;

	/*
	Collider2D hit_collider;
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0) && !hit_collider) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				//Vector3 targetPos = hit.collider.gameObject.transform.position; //Save the position of the object mouse was over
				//Destroy (GameObject.Find (hit.collider.gameObject.name));

				// stop planet
				hit.collider.rigidbody2D.isKinematic = true;
				hit_collider = hit.collider;
			}
		}

		if (Input.GetMouseButtonUp (0)) {

			// release planet again
			if (hit_collider != null) {
				hit_collider.rigidbody2D.isKinematic = false;
			} 
			hit_collider = null;
		}
	}
	*/
	
	void OnMouseDown() {
		dragOrigin = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		gameObject.rigidbody2D.isKinematic = true;
	}

	void OnMouseUp () {
		gameObject.rigidbody2D.isKinematic = false;
	}
	
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + dragOrigin;
		//transform.position = curPosition;

		dragOrigin.z = 0;
		curPosition.z = 0;
		Vector3 direction = dragOrigin - curPosition;
		float intensity = direction.magnitude;
		//direction = direction.normalized;

		Debug.Log ("OnMouseDrag " + dragOrigin + " " + curPosition + " dir: " + direction + " intensity:" + intensity);

	}
}
