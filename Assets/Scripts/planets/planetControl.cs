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
    private int pauseFrame = -1;

    // controls
    private bool held = false;
    private bool drag = false;
    private float holdStartTime = 0;
    private float dragStartTime = 0;
    private float elapsed = 0;

    // 
    private GameObject shipOrbit;
    private tinker tinker;
    private planetSettings planetSettings;

    // tinker
    // Option: restore speed
    private bool slowDownMovement;
    private float slowDownMovementDampingFactor;
    private bool useForcesOption;
    private float useForcesDragAmount;
    private float speedMult;
    private float innerBand;
    private float outerBand;
    private float maxSecForDrag;
    private float maxSecForHold;
    private float forceMult;
    private float acceleration;
    
    //-------------------------------------------------------------------

    void Start()
    {
        shipOrbit = GameObject.Find("shipOrbit");
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        planetSettings = GetComponent<planetSettings>();
    }

    void Update()
    {
        UpdateTinker();

        if (drag){
            print("dragging "  + transform.name);
        } else if (held){
            print("holding "  + transform.name);
        }

        // flicking control
        if (drag && InputReleased())
        {
            print("FLICKED " + transform.name);
            Flick();
        } else if (drag && GetElapsedTime(dragStartTime) > maxSecForDrag)
        { 
            print("DRAG TIMER RAN OUT " + transform.name);
            Release();
        } else if (held && InputReleased())
        {
            print("HELD AND RELEASED " + transform.name);
            Release();
        } else if (held && GetElapsedTime(dragStartTime) > maxSecForHold)
        {
            print("HOLD TIMER RAN OUT "  + transform.name);
            Release();
        }

        if (drag)
        {
            CalculateNewVelocity();
            // release if dragged too far
            if (newVelocity.magnitude > (outerBand / 200)){
                print("DRAGGED TOO FAR " + transform.name);
                Release();
                return;
            }
        }


        if (!held && !drag && useForcesOption){
            RestoreSpeed();
        }
    }
    
    void UpdateTinker()
    {
        speedMult = tinker.PInitSpeedMultiplier;
        slowDownMovement = tinker.PSlowDownMovementOption;
        slowDownMovementDampingFactor = tinker.PSlowDownMovementDampingFactor;
        useForcesOption = tinker.PUseForcesOption;
        useForcesDragAmount = tinker.PUseForcesDragAmount;
        innerBand = tinker.PInnerBand;
        outerBand = tinker.POuterBand;
        maxSecForDrag = tinker.PMaxSecsForDrag;
        maxSecForHold = tinker.PMaxSecsForHold;
        forceMult = tinker.PForceMult;
        acceleration = tinker.PAcceleration;

        if (useForcesOption)
            tinker.PSlowDownMovementOption = true;
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
    }
    
    void OnMouseDrag()
    {
        float dist = Vector3.Distance(storedPosition, InputPosition());
        float radius = GetComponent<CircleCollider2D>().radius;

        print("OnMouseDrag " + transform.name);

        if (dist < radius + (innerBand /200))
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
            rigidbody2D.AddForce(newForce * acceleration);
        }


        /*
        if (useForcesOption)
        {
            rigidbody2D.drag = useForcesDragAmount;
            rigidbody2D.AddForce(new Vector2(planetSettings.speed * speedMult, 0.0f));
        } else
        {
            if (rigidbody2D.velocity.magnitude > storedVelocity.magnitude)
                rigidbody2D.velocity *= slowDownMovementDampingFactor;
        }
        */
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
