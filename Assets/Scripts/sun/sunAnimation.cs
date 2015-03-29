using UnityEngine;
using System.Collections;

public class sunAnimation : MonoBehaviour {
	public bool die;
	private bool dead;
	private GameObject sunBack;
	private GameObject sunFront;
	private GameObject sunWayBack;
    private GameObject backdrop;
	private Animator sunAnim; 
	private Animator sunBackAnim; 
	private Animator sunFrontAnim;
	private Animator sunWayBackAnim; 
    private Vector3 initPosition;
    private Vector3 shakePosition;
    public float shake = 0.0f;

    // tinker
    private tinker tinker;
    private float backdropShakeAmount;

	// Use this for initialization
	void Start () {
		sunBack = GameObject.Find ("sunBackDeath");
		sunFront = GameObject.Find ("sunFrontDeath");;
		sunWayBack = GameObject.Find ("sunWayBackDeath");;

		sunAnim = this.GetComponent<Animator>();
		sunBackAnim = sunBack.GetComponent<Animator>();
		sunFrontAnim = sunFront.GetComponent<Animator>();
		sunWayBackAnim = sunWayBack.GetComponent<Animator>();

        backdrop = GameObject.Find("backdrop");
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        initPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTinker();

		if (!dead && die) 
		{
			dead = true;
			sunAnim.SetBool ("death", true);
			sunBackAnim.SetBool ("death", true);
			sunFrontAnim.SetBool ("death", true);
			sunWayBackAnim.SetBool ("death", true);
		}
        if (shake > 0)
        {
            // backdrop
            shakePosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            backdrop.transform.position = shakePosition  * (shake * backdropShakeAmount);

            // sun
            shakePosition = new Vector3( Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0 );
            transform.position = initPosition + ( shakePosition * shake);
        }
	}

    void UpdateTinker()
    {
        backdropShakeAmount = tinker.GBBackDropShakeAmount;
    }
}
