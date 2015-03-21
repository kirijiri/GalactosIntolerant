using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    Animator anim;
    Animator hoverAnim;
    GameObject hover;
    SpriteRenderer hoverSprite;
    GameObject[] planets;
    bool hold;
    bool flick;
    bool over ;

	/*
	 * COMMENTED OUT ANIM STUFF FOR NOW
	 * 
	 * WILL REPLACE WHEN NEW CONTROLLER IS SET UP
	 * 
	 */



    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        hover = GameObject.Find("shipHover");
        hoverAnim = hover.GetComponent<Animator>();
        hoverSprite = hover.GetComponent<SpriteRenderer>();
        planets = GameObject.FindGameObjectsWithTag("Planet");
        hoverSprite.enabled = false;
    }

    void OnMouseOver(){
        hoverSprite.enabled = true;
    }

    void OnMouseExit(){
        hoverSprite.enabled = false;
    }

    void LateUpdate()
    {
        anim.SetBool("holding", IsHolding());
        anim.SetBool("flicking", IsFlicking());
    }

    public void Open()
    {
        anim.SetBool("open", true);
        hoverAnim.SetBool("open", true);
    }

    public void Close()
    {
        anim.SetBool("open", false);
        hoverAnim.SetBool("open", false);
    }

    private bool IsHolding()
    {
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<planetControl>().held)
            {    
                return true;
            }
        }
        return false;
    }

    private bool IsFlicking()
    {
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<planetControl>().drag)
            {
                return true;
            }
        }
        return false;
    }
}
