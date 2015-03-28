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

    // tinker
    // Option: restore speed
    private bool useForcesOption;
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
        sound = GetComponent<planetSound>();
    }

    void Update()
    {
        UpdateTinker();
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

        if (held)
        {
            scoring.IncreaseDeaths(gameObject);
        }
        if (!held)
        {
            scoring.DecreaseDeaths(gameObject);
        }

        if (!held && !drag && useForcesOption)
        {
            RestoreSpeed();
        }
    }
    
    void UpdateTinker()
    {
        useForcesOption = tinker.PRestoreSpeed;
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
        storedVelocity = rigidbody2D.velocity;
        holdStartTime = Time.time;
        dragStartTime = Time.time;

        // pause
        rigidbody2D.velocity = new Vector3(0, 0, 0);

        Holding();
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
            drag = true;
            held = false;
        }
    }

    //------------------------------------------------------------------- 

    private void Flick()
    {
        newForce = newVelocity.normalized / (GetElapsedTime(dragStartTime) / maxSecForDrag);
        rigidbody2D.velocity = storedVelocity + (newForce * forceMult);

        ResetControlFlags();
        ResetTimers();

        // send message to phone
        phoneMessaging.SendNewMessage(gameObject);

        // play any audio 
        sound.AudioFlick();

        // kill people on flick
        scoring.KillPeople(gameObject);
    }

    private void Holding()
    {
        // Play any audio
        sound.AudioHold();
    }

    private void Release()
    {
        rigidbody2D.velocity = storedVelocity.normalized * acceleration;
        ResetControlFlags();
        ResetTimers();
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
            diffMagnitude = storedVelocity.magnitude - rigidbody2D.velocity.magnitude;
            // if the current velocity is too small, use the stored velocity
            if (rigidbody2D.velocity.magnitude > 0.01)
            {
                newForce = rigidbody2D.velocity.normalized * diffMagnitude;
            } else
            {
                newForce = storedVelocity.normalized * diffMagnitude;
            }
            newForce *= acceleration;
            if (dbug)
            {
                Debug.DrawLine(transform.position, transform.position + newForce, Color.red, 0, false);
            } 
            rigidbody2D.AddForce(newForce);
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
