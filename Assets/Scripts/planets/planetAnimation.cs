using UnityEngine;
using System.Collections;

public class planetAnimation : MonoBehaviour {
    private Animator planetAnim;
    private Animator glowAnim;
    private Animator elecOverAnim;
    private Animator elecUnderAnim;
    private Animator holdAnim;
    private Animator hoverAnim;

    private planetSettings settings;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        planetAnim = transform.Find("planet_graphic").GetComponent<Animator>();
        glowAnim = transform.Find("glow_graphic").GetComponent<Animator>();
        elecOverAnim = transform.Find("elecOver_graphic").GetComponent<Animator>();
        elecUnderAnim = transform.Find("elecUnder_graphic").GetComponent<Animator>();
        holdAnim = transform.Find("hold_graphic").GetComponent<Animator>();
        hoverAnim = transform.Find("hover_graphic").GetComponent<Animator>();
        
        settings = GetComponent<planetSettings>();
	}
	
	void Update()
    {
        planetAnim.SetFloat("decay", (float)(settings.population / settings.maxPopulation));
    }

    public void Holding(bool hold)
    {
        holdAnim.SetBool("hold", hold);
    }

    public void InBeam(bool inBeam)
    {
        elecOverAnim.SetBool("inBeam", inBeam);
        elecUnderAnim.SetBool("inBeam", inBeam);
        glowAnim.SetBool("inBeam", inBeam);
    }
    
    public void Aligned(bool aligned)
    {
        elecOverAnim.SetBool("aligned", aligned);
        elecUnderAnim.SetBool("aligned", aligned);
    }

    public void Hover(bool hovering)
    {
        hoverAnim.SetBool("hover", hovering);
    }
}
