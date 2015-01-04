using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class shipInitialiser : MonoBehaviour {
	Vector3 newVec;
	Vector3 newPos;
	Vector3 curPos;
	Vector3 centre;
	public float orbitRadius;
		
	void Start () {
		GameObject sun = GameObject.Find ("sun");
		centre = sun.transform.position;
	}

	void Awake(){
		SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();
		GameObject imagePrefab = Instantiate (Resources.Load ("image_prefab")) as GameObject;
		
		// setup a image prefab, then turn off sprite
		imagePrefab.name = gameObject.name + "_IMAGE";
		imagePrefab.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
		imagePrefab.GetComponent<connectImageToControl> ().parent = gameObject;
		sprRen.enabled = false;

		// lookat sun
		imagePrefab.GetComponent<connectImageToControl> ().lookat = GameObject.Find("sun");	

		// TEMP!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		imagePrefab.GetComponent<SpriteRenderer> ().color = Color.red;

	}

	void OnMouseDrag () {
		curPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
		curPos.z = 0;
		newVec = curPos - centre;
		newPos = newVec.normalized * (orbitRadius / 100 / 2);
		transform.position = newPos + centre;
	}

}
