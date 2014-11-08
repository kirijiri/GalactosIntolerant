using UnityEngine;
using System.Collections;

public class planetTouch : MonoBehaviour {
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
}
