using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    private Animator anim;
    private Animator hoverAnim;
    private Animator guideAnim;
    private GameObject hover;
    private GameObject[] planets;
    private SpriteRenderer hoverSprite;
    private bool hold;
    private bool flick;
    private bool over ;

    // tinkered
    private tinker tinker;
    private float beamActivateAngle;
    private float minDragDistance;
    private float maxGuideDragDistance;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        hover = GameObject.Find("shipHover");
        hoverAnim = hover.GetComponent<Animator>();
        guideAnim = GameObject.Find("beamGuide").GetComponent<Animator>();
        planets = GameObject.FindGameObjectsWithTag("Planet");
        hoverSprite = hover.GetComponent<SpriteRenderer>();
        hoverSprite.enabled = false;
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
    }

    void Update(){
        UpdateTinker();
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

    // PUBLIC --------------------------------------------------------------------------------

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

    public void GuideOn()
    {
        guideAnim.SetBool("enable", true);
    }

    public void GuideOff()
    {
        guideAnim.SetBool("enable", false);
    }

    public void GuideDetails(float dist, float angle)
    {
        print(dist);
        guideAnim.SetBool("inAngle", angle < beamActivateAngle);
        guideAnim.SetFloat("dist", ((dist * 200.0f) - minDragDistance) / (maxGuideDragDistance - minDragDistance));
    }

    // PRIVATE --------------------------------------------------------------------------------

    void UpdateTinker()
    {
        beamActivateAngle = tinker.GBActivateAngle;
        minDragDistance = tinker.GBMinDragDistance;
        maxGuideDragDistance = tinker.GBMaxGuideDragDistance;
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
