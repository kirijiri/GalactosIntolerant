using UnityEngine;
using System.Collections;

public class gravityBeamAnimation : MonoBehaviour {
	private sunAnimation sunAnimation;

	// Use this for initialization
	void Start () {
		sunAnimation = GameObject.Find ("sun").GetComponent<sunAnimation>();
	}
	
	public void BeamAnimationOn(){
		sunAnimation.die = true;
	}
}
