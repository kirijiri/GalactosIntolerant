using UnityEngine;
using System.Collections;

public class buildLevel : MonoBehaviour {
	private Sprite[] pl_sprites;
	private GameObject planet;
	
	// Use this for initialization
	void Start () {
		// Build array on sprites
		pl_sprites = Resources.LoadAll<Sprite>("Planets");

		for (int i=0; i<pl_sprites.Length; i++) {
			HingeJoint2D hinge;
			JointMotor2D motor;
			SpriteRenderer sprite_rndr;

			planet = Instantiate(Resources.Load("planets_prefab")) as GameObject;

			// Edit the hinge
			hinge = planet.GetComponent<HingeJoint2D>();
			hinge.anchor = new Vector2(i+1,0);
			motor = hinge.motor;
			motor.motorSpeed = 500.0f;
			hinge.motor = motor;

			// Change the Sprite
			sprite_rndr = planet.GetComponent<SpriteRenderer>();
			sprite_rndr.sprite = pl_sprites[i] ;

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
