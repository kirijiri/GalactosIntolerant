using UnityEngine;
using System.Collections;

public class shipAnimation : MonoBehaviour
{
    private Animator anim;
    private Animator hoverAnim;
    private Animator guideAnim;
    private GameObject hover;
    private GameObject coneL;
    private GameObject coneR;
    private SpriteRenderer coneLSpriteRend;
    private SpriteRenderer coneRSpriteRend;
    private GameObject[] planets;
    private SpriteRenderer hoverSprite;
    private bool hold;
    private bool flick;
    private bool over ;

    // tinkered
    private tinker tinker;
    private float beamActivateAngle;
    private float minGuideDistance;
    private float maxGuideDistance;

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

        coneL = GameObject.Find("cone_L_OFS");
        coneR = GameObject.Find("cone_R_OFS");
        coneLSpriteRend = GameObject.Find("cone_L").GetComponent<SpriteRenderer>();
        coneRSpriteRend = GameObject.Find("cone_R").GetComponent<SpriteRenderer>();
        coneLSpriteRend.enabled = false;
        coneRSpriteRend.enabled = false;
    }



    void Update(){
        UpdateTinker();

        // cone guide
        coneL.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -beamActivateAngle));
        coneR.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, beamActivateAngle));
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
        coneLSpriteRend.enabled = true;
        coneRSpriteRend.enabled = true;
    }

    public void Close()
    {
        anim.SetBool("open", false);
        hoverAnim.SetBool("open", false);
        coneLSpriteRend.enabled = false;
        coneRSpriteRend.enabled = false;
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
        guideAnim.SetBool("inAngle", angle < beamActivateAngle);
        guideAnim.SetFloat("dist", ((dist * 200.0f) - minGuideDistance) / (maxGuideDistance - minGuideDistance));
    }

    // PRIVATE --------------------------------------------------------------------------------

    void UpdateTinker()
    {
        beamActivateAngle = tinker.GBActivateAngle;
        minGuideDistance = tinker.GBMinGuideDistance;
        maxGuideDistance = tinker.GBMaxGuideDistance;
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
