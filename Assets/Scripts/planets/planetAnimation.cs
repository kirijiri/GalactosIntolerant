using UnityEngine;
using System.Collections;

public class planetAnimation : MonoBehaviour {
    private Animator planetAnim;
    private Animator fxAnim;
    private planetSettings settings;

	// Use this for initialization
	void Start () {
        planetAnim = transform.Find("planet_graphic").GetComponent<Animator>();
        fxAnim = transform.Find("fx_graphic").GetComponent<Animator>();
        settings = GetComponent<planetSettings>();
	}
	
	// Update is called once per frame
	void Update()
    {
        planetAnim.SetFloat("decay", (float)(settings.population / settings.maxPopulation));
    }

    public void Hover()
    {

    }

    public void Hold()
    {

    }

    public void Flick()
    {

    }

    public void FXOff()
    {

    }

    public void GravityOn()
    {

    }

    public void Aligned()
    {

    }
}
