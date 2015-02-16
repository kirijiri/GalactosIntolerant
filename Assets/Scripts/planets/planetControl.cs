using UnityEngine;
using System.Collections;

public class planetControl : MonoBehaviour
{
    private Vector3 storedPosition;
    private Vector3 storedVelocity;
    private float storedAngularVelocity;
    private Vector3 newVelocity;

    // controls
    private bool held = false;
    private bool drag = false;
    private bool flicked = false;

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

        // flicking control
        if (drag && InputReleased())
        {
            rigidbody2D.velocity = newVelocity;
            ResetControlFlags();
            flicked = true;
            return;
        }
        
        // holding control
        if (held && InputReleased())
        {
            rigidbody2D.velocity = storedVelocity;
            ResetControlFlags();
            return;
        }

        if (drag)
        {
            CalculateNewVelocity();
        }

        if (slowDownMovement && (rigidbody2D.velocity.magnitude <= storedVelocity.magnitude) && storedVelocity.magnitude > 0)
        {
            rigidbody2D.drag = 0;
        }

        if (flicked && slowDownMovement)
        {
            RestoreSpeed();
            if (useForcesOption) flicked = false;
            return;
        }
    }
    
    void UpdateTinker()
    {
        slowDownMovement = tinker.PSlowDownMovementOption;
        slowDownMovementDampingFactor = tinker.PSlowDownMovementDampingFactor;
        useForcesOption = tinker.PUseForcesOption;
        useForcesDragAmount = tinker.PUseForcesDragAmount;

        if (useForcesOption)
            tinker.PSlowDownMovementOption = true;
    }

    void OnMouseDown()
    {
        // store data
        storedPosition = transform.position;
        storedVelocity = rigidbody2D.velocity;

        // pause
        rigidbody2D.velocity = new Vector3(0, 0, 0);
    }
    
    void OnMouseDrag()
    {
        float dist = Vector3.Distance(storedPosition, InputPosition());
        float radius = GetComponent<CircleCollider2D>().radius;

        if (dist < radius)
        { 
            held = true;
        } else
        {
            drag = true;
        }
    }

    //------------------------------------------------------------------- 

    private void CalculateNewVelocity() 
    {
        newVelocity = InputPosition() - storedPosition;
    }

    private void RestoreSpeed()
    {
        if (useForcesOption)
        {
            rigidbody2D.drag = useForcesDragAmount;
            rigidbody2D.AddForce(new Vector2(planetSettings.speed, 0.0f));
        }
        else{
            if (rigidbody2D.velocity.magnitude > storedVelocity.magnitude)
                rigidbody2D.velocity *= slowDownMovementDampingFactor;
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

    private void ResetControlFlags()
    { 
        held = false; 
        drag = false; 
    }
}
