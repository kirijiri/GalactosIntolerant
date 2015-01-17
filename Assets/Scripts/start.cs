using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class start : MonoBehaviour {

	void OnMouseDown () {
		Application.LoadLevel ("scene1");
	}
}
