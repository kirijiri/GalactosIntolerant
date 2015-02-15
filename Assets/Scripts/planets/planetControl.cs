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
            print ("drag");
            CalculateNewVelocity();
        }

        if (flicked && slowDownMovement)
        {
            RestoreSpeed();
            return;
        }
    }
    
    void UpdateTinker()
    {
        slowDownMovement = tinker.PSlowDownMovementOption;
        slowDownMovementDampingFactor = tinker.PSlowDownMovementDampingFactor;
    }

    void OnMouseDown()
    {
        // store data
        storedPosition = transform.position;
        storedVelocity = rigidbody2D.velocity;

        // pause
        rigidbody2D.velocity = new Vector3(0, 0, 0);

        // tell ship orbit to not respond
        shipOrbit.SendMessage("SetIsOn", false);
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
        // tell ship orbit to not respond
        shipOrbit.SendMessage("SetIsOn", false);
    }

    void OnMouseUp()
    {
        // turn the ship Obit on again
        shipOrbit.SendMessage("SetIsOn", true);
    }

    //------------------------------------------------------------------- 

    private void CalculateNewVelocity() 
    {
        newVelocity = InputPosition() - storedPosition;
    }

    private void RestoreSpeed()
    {
        if (rigidbody2D.velocity.magnitude > storedVelocity.magnitude)
            rigidbody2D.velocity *= slowDownMovementDampingFactor;
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
