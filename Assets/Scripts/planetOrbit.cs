using UnityEngine;
using System.Collections;

public class planetOrbit : MonoBehaviour {
	public float radius;
	public float startAngle;
	public float speed;
	public bool animate = true;
	private float angle;

	// Use this for initialization
	void Start () {
		angle = startAngle;
		SetPosition();
	}
	
	// Update is called once per frame
	void Update () {
		if (animate) 
		{
			angle += speed;
			SetPosition ();
		}
	}

	private void SetPosition () {
		transform.localPosition = Quaternion.AngleAxis(angle, new Vector3(0,0,1)) * new Vector3(radius,0,0);
	}
}
