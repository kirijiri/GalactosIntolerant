using UnityEngine;
using System.Collections;

public class shipOrbitControl : MonoBehaviour {
	bool largeHit = false;
	bool smallHit = false;

	void LateUpdate () {
		largeHit = false;
		smallHit = false;
	}
	
	void LargeColHit(){
		largeHit = true;
		print ("large hit");
	}
	void SmallColHit(){
		smallHit = true;
		print ("small hit");
	}
}
