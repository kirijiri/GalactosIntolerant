using UnityEngine;
using System.Collections;

// This class sets up the planet prefab structure

public class planetInit : MonoBehaviour
{
    // velocity that the planet tries to maintain
    public Vector2 initVelocity;
    private Vector3 initDirection;
    public float bleedMultilier = 1.0f;
    public float gravityBleedMultilier = 1.0f;
    public bool do_kill_people = false;

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
    private bool randomiseInitialPosition;
    private bool randomiseInitialDirection;
	private bool randomiseInitialSpeed;
	private float randomUpperSpeed;
	private float randomLowerSpeed;

    //-------------------------------------------------------------------

    void Awake()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        sun = GameObject.Find("sun");
    }

    void Start()
    {
        UpdateTinker();

        // get settings 
        planetSettings = GetComponent<planetSettings>();
        planetSettings.maxPopulation = planetSettings.population;
        initSpeed = planetSettings.speed * speedMult;
        orbitRadius = planetSettings.orbitRadius;

        if (randomiseInitialPosition)
        {
            posDiff = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f), 0.0f);
        }
        else
        {
            posDiff = transform.localPosition;
        }
        posDiff = posDiff.normalized * (orbitRadius / 200);

        // setup physics
        HingeSetup();
        InitVelocity();
        ColliderSetup();
        IgnoreCollisions();
    }

    void Update(){
        UpdateTinker();

        if (initVelocity.magnitude == 0 && Time.timeSinceLevelLoad > 1)
        {
            initVelocity = GetComponent<Rigidbody2D>().velocity;
        }
    }

    void UpdateTinker(){
        speedMult = tinker.PInitSpeedMultiplier;
        randomiseInitialPosition = tinker.PRandomiseInit;
        randomiseInitialDirection = tinker.PRandomiseDirection;

		randomiseInitialSpeed = tinker.PRandomiseSpeed;
		randomLowerSpeed = tinker.PRandomLowerSpeed;
		randomLowerSpeed = tinker.PRandomUpperSpeed;
    }

    //------------------------------------------------------------------- physics

    private void HingeSetup()
    {
        HingeJoint2D hinge = gameObject.GetComponent<HingeJoint2D>();
        hinge.connectedBody = sun.GetComponent<Rigidbody2D>();
        hinge.anchor = new Vector2(posDiff.x, posDiff.y);
    }

    private void InitVelocity()
    {
		if (randomiseInitialSpeed) 
		{
			initSpeed = Random.Range(randomLowerSpeed, randomUpperSpeed);
		}

        initDirection = Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * posDiff.normalized;
        if (randomiseInitialDirection && Random.Range(-1.0f, 1.0f) < 0)
        {
            initSpeed *= -1;
        }
        GetComponent<Rigidbody2D>().velocity = initDirection * initSpeed;
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
            Physics2D.IgnoreCollision(planet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        GameObject satellite = GameObject.Find("ship");
        Physics2D.IgnoreCollision(satellite.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}