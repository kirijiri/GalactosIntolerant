using UnityEngine;
using System.Collections;

public class gravityBeamAnimation : MonoBehaviour {
    private Animator anim;
	private sunAnimation sunAnimation;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
		sunAnimation = GameObject.Find ("sun").GetComponent<sunAnimation>();
	}
	
	public void BeamAnimationOn(){
		sunAnimation.die = true;
        anim.SetBool("on", true);
	}
}
