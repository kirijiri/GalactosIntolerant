using UnityEngine;
using System.Collections;

public class planetAnimation : MonoBehaviour {
    private Animator anim;
    private planetSettings settings;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        anim = GetComponent<planetInit>().planetGraphic.GetComponent<Animator>();
        spriteRenderer = GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        settings = GetComponent<planetSettings>();
	}
	
	// Update is called once per frame
	void Update(){
        anim.SetFloat("decay", (float)(settings.population / settings.maxPopulation));
    }

    public void Gravity(bool active){
        if (active)
        {
            spriteRenderer.color = Color.red;
        } else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
