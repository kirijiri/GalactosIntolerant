using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class restart : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown () {
		Application.LoadLevel ("titleScreen");
	}
}
