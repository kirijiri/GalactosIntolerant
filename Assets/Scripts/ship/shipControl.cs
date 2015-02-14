using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour {
	public bool gravityBeamEngaged = false;

	tinker tinker;
	bool hold = false;
	float dot;
	Vector3 storedMousePosition;
	Vector3 mouseDrag;

	// tinkered
	float beamActivateAngle;
	
	void Start () {
		tinker = GameObject.Find ("tinker").GetComponent<tinker>();
	}

	void Update () {
		UpdateTinker ();
	}
	
	void UpdateTinker () {
		beamActivateAngle = tinker.beamActivateAngle;
	}
	
	// events -------------------------------------------------------------

	void OnMouseDown() {
		storedMousePosition = transform.position;
	}

	void OnMouseDrag() {
		hold = true;
	}
	
	void OnMouseUp(){
		// Calculate the drag manually and activcate beam if in angle
		if (GetDragAngle() < beamActivateAngle ){
			ActivateGravityBeam();
		}
		else{
			hold = false;
		}
	}

	// Private functions ------------------------------------------------------------------

	private Vector3 GetInputPosition() {
		Vector3 camPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		camPosition.z = 0;
		return camPosition;
	}

	private float GetDragAngle() {
		mouseDrag = GetInputPosition() - storedMousePosition;
		dot = Vector3.Dot(transform.position.normalized, mouseDrag.normalized);
		return ConvertDotToAngle(dot);
	}

	private float ConvertDotToAngle(float nb) {
		// changes scale from -1:1 to 180:0
		return (-nb/2f + 0.5f)*180f;
	}

	private void ActivateGravityBeam() {
		GetComponent<SpriteRenderer>().color = Color.white;
		gravityBeamEngaged = true;
		hold = true;
		
		snapshot snapshot = GameObject.Find ("phone_button_32").GetComponent<snapshot>();
		snapshot.renderer.enabled = true;
	}
	
}









//	public float speed = 200f;
//	public bool gravityBeamEngaged = false;
//	public float gravityBeamThreshold = 0.3f;
//	public float alignmentThreshold = 0.00005f;
//	public float beamActivateAngle = 30f;
//	
//	GameObject sun;
//
//	float orbitPixelRadius;
//	float currentAngle;
//	float targetAngle;
//	float diffAngle;
//	float currentDiffAngle;
//	float dot;
//
//	bool hold = false;
//	
//	Vector3 sunPos;
//	Vector3 solarPos;
//	Vector3 toRotate;
//	Vector3 axis = new Vector3(0,0,1);
//	Vector3 diffAxis;
//	Vector3 storedMousePosition;
//	Vector3 mouseDrag;
//
//	Quaternion currentQuat;
//	Quaternion originalQuat;
//	Quaternion targetQuat;
//	Quaternion diffQuat;
//	Quaternion moveQuat;
//
//	// Overwrite methods ----------------------------------------------------
//
//	void Start(){
//		sun = GameObject.Find ("sun");
//
//		sunPos = sun.transform.position;
//		orbitPixelRadius = gameObject.GetComponent<shipInitialiser>().orbitRadius / 100 / 2;
//		toRotate = new Vector3(0, orbitPixelRadius, 0);
//
//		SetCurrentData();
//		SetPosByQuat(currentQuat);
//	}
//
//	void Update(){
//		SetCurrentData();
//		currentDiffAngle = GetAngleToTarget();
//
//		if (hold == false && currentDiffAngle != 0){
//			moveQuat = Quaternion.RotateTowards(currentQuat, targetQuat, speed * Time.deltaTime);
//			SetPosByQuat(moveQuat);
//		}
//	}
//
//
//	// Getters -------------------------------------------------------------
//
//	Vector3 GetSolarPosition(){
//		return transform.position - sunPos;
//	}
//
//	Quaternion GetQuatFromAngle(float angle){
//		return Quaternion.AngleAxis(angle-90, axis);
//	}
//
//	float GetCurrentAngle(){
//		solarPos = GetSolarPosition();
//		return Mathf.Atan2 (solarPos.y, solarPos.x) * Mathf.Rad2Deg;
//	}
//
//	float GetAngleToTarget(){
//		diffQuat = Quaternion.Inverse(targetQuat) * currentQuat;
//		diffQuat.ToAngleAxis(out diffAngle, out diffAxis);
//		return diffAngle;
//	}
//
//	Vector3 GetInputPosition() {
//		Vector3 camPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
//		camPosition.z = 0;
//		return camPosition;
//	}
//	
//	// Setters -------------------------------------------------------------
//
//	void SetCurrentData(){
//		currentAngle = GetCurrentAngle();
//		currentQuat = GetQuatFromAngle(currentAngle);
//	}
//	
//	void SetPosByQuat(Quaternion quat){
//		transform.position = (quat * toRotate) + sunPos;
//	}
//
//	// Commands ------------------------------------------------------------
//
//	void MoveTo(float angle){
//		originalQuat = currentQuat;
//		targetAngle = angle;
//		targetQuat = GetQuatFromAngle(targetAngle);
//	}
//}
