using UnityEngine;
using System.Collections;

// This class sets up the planet prefab structure

public class planetInit : MonoBehaviour
{
    // graphic object to be able to rotate the  
    // planet independent from the sprite
    public GameObject planetGraphic;
    
    // get planet settings from settings class (easier to set up)
    private planetSettings planetSettings;

    // private
    private GameObject sun;
    private float initSpeed;
    private float orbitRadius;
    private Vector3 posDiff;
    private SpriteRenderer sprRen;
    private tinker tinker;

    //tinkered
    private float speedMult;

    //-------------------------------------------------------------------

    void Awake()
    {
        // change image sprite
        sprRen = GetComponent<SpriteRenderer>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        MakeImagePrefab();
    }

    void Start()
    {
        UpdateTinker();
        // get settings 
        planetSettings = GetComponent<planetSettings>();
        initSpeed = planetSettings.speed * speedMult;
        orbitRadius = planetSettings.orbitRadius;

        // get pos from sun
        sun = GameObject.Find("sun");
        posDiff = (sun.transform.position - transform.position);
        posDiff.z = 0;

        // resize the posDiff to snap to the radius
        posDiff = posDiff.normalized * (orbitRadius / 100 / 2);

        // setup physics
        HingeSetup();
        InitVelocity();
        ColliderSetup();
        IgnoreCollisions();
    }

    void Update(){
        UpdateTinker();
    }

    void UpdateTinker(){
        speedMult = tinker.PInitSpeedMultiplier;
    }

    //------------------------------------------------------------------- physics

    private void HingeSetup()
    {
        HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
        Vector3 scale = transform.localScale;

        // edit the hinge
        hinge.anchor = new Vector2(posDiff.x / (1 * scale.x), posDiff.y / (1 * scale.x));
        hinge.connectedBody = sun.rigidbody2D;
    }

    private void InitVelocity()
    {
        Vector3 initDirection = posDiff.normalized;
        initDirection = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * initDirection;

        rigidbody2D.velocity = initDirection * initSpeed;
    }

    private void ColliderSetup()
    {
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();
        string sprName = sprRen.sprite.name.ToString();
        float sprRad = System.Convert.ToSingle(sprName.Split('_') [2]);

        collider.radius = sprRad / 100 / 2;
    }

    private void IgnoreCollisions()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        // ignore all the planets
        foreach (GameObject planet in planets)
        {
            Physics2D.IgnoreCollision(planet.collider2D, collider2D);
        }
        GameObject satellite = GameObject.Find("ship");
        Physics2D.IgnoreCollision(satellite.collider2D, collider2D);
    }

    //------------------------------------------------------------------- sprite

    private void MakeImagePrefab()
    {
        planetGraphic = Instantiate(Resources.Load("image_prefab")) as GameObject;
        
        // setup a image prefab, then turn off sprite
        planetGraphic.name = name + "_IMAGE";
        planetGraphic.GetComponent<SpriteRenderer>().sprite = sprRen.sprite;
        planetGraphic.GetComponent<planetImageSpriteControl>().parent = gameObject;
        sprRen.enabled = false;
    }
}