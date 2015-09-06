using UnityEngine;
using System.Collections;

public class message : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		print (sr.sprite.rect);
		print (sr.sprite.bounds);
		print ((float)gameObject.transform.localScale.x);
		print (gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
