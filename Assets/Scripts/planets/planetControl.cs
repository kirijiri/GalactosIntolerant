using UnityEngine;
using System.Collections;

public class planetControl : MonoBehaviour
{
    private Vector3 storedPosition;
    private Vector3 storedVelocity;
    private Vector3 newVelocity;
    private Vector3 newForce;
    private float storedAngularVelocity;
    private float diffMagnitude;
    private float targetSpeed;
    private bool returnToTargetSpeed;
    private bool targetSpeedClockwise;
    private bool needToAccelerate = true;
    private GameObject[] allPlanets;
    private bool otherPlanetDragged = false;

    // controls
    public bool held = false;
    public bool drag = false;
    private float holdStartTime = 0;
    private float dragStartTime = 0;

    // 
    private tinker tinker;
    private phoneMessages phoneMessaging;
    private scoring scoring;
    private planetSound sound;
    private planetAnimation anim;
    private planetInit planetInit;

    // tinker
    private float innerBand;
    private float outerBand;
    private float maxSecForDrag;
    private float maxSecForHold;
    private float forceMult;
    private float acceleration;
    private bool dbug;
    private bool flickOnOuterBand;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        phoneMessaging = GameObject.Find("messages").GetComponent<phoneMessages>();
        scoring = GameObject.Find("Main Camera").GetComponent<scoring>();
        sound = GameObject.Find("planets").GetComponent<planetSound>();
        anim = GetComponent<planetAnimation>();
        planetInit = GetComponent<planetInit>();
        allPlanets = GameObject.FindGameObjectsWithTag("Planet");
    }

    void Update()
    {
        UpdateTinker();

        // grab init velocity when it's there
        if (storedVelocity.magnitude == 0 && planetInit.initVelocity.magnitude > 0)
        {
            storedVelocity = planetInit.initVelocity;
        }

        // debug printing
        if (dbug)
        {
            if (drag)
            {
                print("dragging " + transform.name);
            } else if (held)
            {
                print("holding " + transform.name);
            }
        }

        // flicking control
        if (drag && InputReleased())
        {
            if (dbug)
            {
                print("FLICKED " + transform.name);
            }
            Flick();
        } else if (drag && GetElapsedTime(dragStartTime) > maxSecForDrag)
        { 
            if (dbug)
            {
                print("DRAG TIMER RAN OUT " + transform.name);
            }
            Release();
        } else if (held && InputReleased())
        {
            if (dbug)
            {
                print("HELD AND RELEASED " + transform.name);
            }
            Release();
        } else if (held && GetElapsedTime(holdStartTime) > maxSecForHold)
        {
            if (dbug)
            {
                print("HOLD TIMER RAN OUT " + transform.name);
            }
            Release();
        }

        if (drag)
        {
            CalculateNewVelocity();
            // release if dragged too far
            if (newVelocity.magnitude > (outerBand / 200))
            {
                if (dbug)
                {
                    print("DRAGGED TOO FAR " + transform.name);
                }
                if (flickOnOuterBand)
                {
                    Flick();
                } else
                {
                    Release();
                }
                return;
            }
        }

        // scoring
        if (held)
        {
            scoring.IncreaseDeaths(gameObject);
        }
        if (!held)
        {
            scoring.DecreaseDeaths(gameObject);
        }

        // animation
        anim.Holding(held || drag);

		// restore speed if needed
        if (!held && !drag && needToAccelerate)
        {
            RestoreSpeed();
        }
    }
    
    void UpdateTinker()
    {
        innerBand = tinker.PInnerBand;
        outerBand = tinker.POuterBand;
        maxSecForDrag = tinker.PMaxSecsForDrag;
        maxSecForHold = tinker.PMaxSecsForHold;
        forceMult = tinker.PForceMult;
        acceleration = tinker.PAcceleration;
        dbug = tinker.printPlanetControls;
        flickOnOuterBand = tinker.PFlickOnOuterBand;
    }

    void OnMouseDown()
    {

        // store data
        storedPosition = transform.position;
        storedVelocity = GetComponent<Rigidbody2D>().velocity;
        holdStartTime = Time.time;

        // pause
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

        sound.AudioHold();
    }
    
    void OnMouseDrag()
    {
        float dist = Vector3.Distance(storedPosition, InputPosition());
        float radius = GetComponent<CircleCollider2D>().radius;

        if (dist < radius + (innerBand / 200))
        { 
            drag = false;
            held = true;
        } else
        {
			// only capture drag time when first start dragging
			if (!drag)
			{
				dragStartTime = Time.time;
			}
            drag = true;
            held = false;
        }
    }

    void OnMouseOver()
    {
        // check to see whether a different planet is being dragged
        otherPlanetDragged = false;
        foreach (GameObject _planet in allPlanets)
        {
            if (_planet.GetComponent<planetControl>().drag)
            {
                otherPlanetDragged = true;
                break;
            }
        }

        // only show hover if another planet isn't being dragged
        if (otherPlanetDragged == false)
        {
            anim.Hover(true);
        }
    }
    
    void OnMouseExit()
    {
        anim.Hover(false);
    }

    //------------------------------------------------------------------- 

    private void Flick()
    {
        newForce = newVelocity.normalized / (GetElapsedTime(dragStartTime) / maxSecForDrag);
        GetComponent<Rigidbody2D>().velocity = storedVelocity + (newForce * forceMult);

        ResetControlFlags();
        ResetTimers();

        // send message to phone
        phoneMessaging.SendNewMessage(gameObject);

        // play any audio 
        sound.AudioFlick();

        // kill people on flick
        scoring.KillPeople(gameObject);

        // 
        needToAccelerate = false;
    }

    private void Release()
    {
        ResetControlFlags();
        ResetTimers();
        needToAccelerate = true;
        
        sound.AudioHoldRelease();
    }

    private float GetElapsedTime(float start)
    {
        return Time.time - start;
    }

    private void CalculateNewVelocity()
    {
        newVelocity = InputPosition() - storedPosition;
    }

    private void RestoreSpeed()
    {
        if (storedVelocity.magnitude > 0.01)
        {
            // if we're at the right speed, ignore
            if (GetComponent<Rigidbody2D>().velocity.magnitude == storedVelocity.magnitude)
            {
                needToAccelerate = false;
                return;
            }

            diffMagnitude = storedVelocity.magnitude - GetComponent<Rigidbody2D>().velocity.magnitude;
            // if the current velocity is too small, use the stored velocity
            if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.01)
            {
                newForce = GetComponent<Rigidbody2D>().velocity;
            } else
            {
                newForce = storedVelocity;
            }
            newForce = newForce.normalized  * diffMagnitude;

            newForce *= acceleration;
            if (dbug)
            {
                Debug.DrawLine(transform.position, transform.position + newForce, Color.red, 0, false);
            } 
            GetComponent<Rigidbody2D>().AddForce(newForce);
        }
    }

    private Vector3 InputPosition()
    {
        Vector3 camPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        camPosition.z = 0;
        return camPosition;
    }

    private bool InputReleased()
    { 
        return Input.GetMouseButtonUp(0);
    }

    private void ResetTimers()
    {
        holdStartTime = 0;
        dragStartTime = 0;
    }

    private void ResetControlFlags()
    { 
        held = false; 
        drag = false; 
    }
}
