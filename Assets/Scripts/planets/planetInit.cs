using UnityEngine;
using System.Collections;

// This class sets up the planet prefab structure

public class planetInit : MonoBehaviour
{
    // get planet settings from settings class (easier to set up)
    private planetSettings planetSettings;

    // private
    private GameObject sun;
    private float initSpeed;
    private float orbitRadius;
    private Vector3 posDiff;
    private SpriteRenderer sprRen;
    private tinker tinker;
    private Animator anim;

    //tinkered
    private float speedMult;
    private bool randomiseInitialPosition;

    //-------------------------------------------------------------------

    void Awake()
    {
        // change image sprite
        sprRen = GetComponent<SpriteRenderer>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        UpdateTinker();

        // get settings 
        sun = GameObject.Find("sun");
        planetSettings = GetComponent<planetSettings>();
        planetSettings.maxPopulation = planetSettings.population;
        initSpeed = planetSettings.speed * speedMult;
        orbitRadius = planetSettings.orbitRadius;

        // resize the posDiff to snap to the radius
        if (randomiseInitialPosition)
        {
            Random.seed = System.DateTime.Now.Minute + System.DateTime.Now.Millisecond + transform.GetHashCode();
            posDiff = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f), 0.0f).normalized;
        }
        else
        {
            posDiff = transform.localPosition.normalized;
        }
        posDiff *= orbitRadius / 200;

        // setup physics
        HingeSetup();
        InitVelocity();
        ColliderSetup();
        IgnoreCollisions();
    }

    void Update()
    {
        UpdateTinker();
    }

    void UpdateTinker()
    {
        speedMult = tinker.PInitSpeedMultiplier;
        randomiseInitialPosition = tinker.PRandomiseInit;
    }

    //------------------------------------------------------------------- physics

    private void HingeSetup()
    {
        HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
        hinge.anchor = new Vector2(-posDiff.x, -posDiff.y);
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
        collider.radius = planetSettings.size / 200.0f;
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
}
