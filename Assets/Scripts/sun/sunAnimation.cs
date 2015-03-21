using UnityEngine;
using System.Collections;

public class sunAnimation : MonoBehaviour {
	public bool die;
	private bool dead;
	private GameObject sunBack;
	private GameObject sunFront;
	private GameObject sunWayBack;
	private Animator sunAnim; 
	private Animator sunBackAnim; 
	private Animator sunFrontAnim;
	private Animator sunWayBackAnim; 


	// Use this for initialization
	void Start () {
		sunBack = GameObject.Find ("sunBackDeath");
		sunFront = GameObject.Find ("sunFrontDeath");;
		sunWayBack = GameObject.Find ("sunWayBackDeath");;

		sunAnim = this.GetComponent<Animator>();
		sunBackAnim = sunBack.GetComponent<Animator>();
		sunFrontAnim = sunFront.GetComponent<Animator>();
		sunWayBackAnim = sunWayBack.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead && die) 
		{
			dead = true;
			sunAnim.SetBool ("death", true);
			sunBackAnim.SetBool ("death", true);
			sunFrontAnim.SetBool ("death", true);
			sunWayBackAnim.SetBool ("death", true);
		}
	}
}
