using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour
{
    private float dot;
    private Vector3 storedPosition;
    private Vector3 mouseDrag;
    public bool isMoving = true;
    private bool isBeingDragged = false;
    public bool gravityBeamActivated = false;

    // tinkered
    private tinker tinker;
    private float beamActivateAngle;
    private float minDragDistance;

    // gravity beam
    private gravityBeam gravityBeam;
    private shipAnimation shipAnim;
    private thrustAnimation thusterAnimation;

    //-------------------------------------------------------------------
    
    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
        shipAnim = this.GetComponent<shipAnimation>();
    }

    void Update()
    {
        UpdateTinker();
    }
    
    void UpdateTinker()
    {
        beamActivateAngle = tinker.GBActivateAngle;
        minDragDistance = tinker.GBMinDragDistance / 200.0f;
    }
    
    // events -------------------------------------------------------------

    void OnMouseDown()
    {
        storedPosition = transform.position;
        isMoving = false;
        shipAnim.Open();
        shipAnim.GuideOn();
    }

    void OnMouseDrag()
    {
        isMoving = false;
        isBeingDragged = true;

        shipAnim.GuideDetails(GetDragDistance(), GetDragAngle());

    }
    
    void OnMouseUp()
    {
        // Calculate the drag manually and activate beam if in angle
        if (GetDragDistance() > minDragDistance)
        {
            if (GetDragAngle() < beamActivateAngle)
            {
                ActivateGravityBeam();
                shipAnim.GuideOff();
            }
          
        }
            
        isMoving = true;
        shipAnim.Close();
        shipAnim.GuideOff();
        isBeingDragged = false;
    }

    void OnMouseOver()
    {
        isMoving = false;
    }
    
    void OnMouseExit()
    {
        isMoving = true;
    }

    // Private functions ------------------------------------------------------------------

    private float GetDragDistance()
    {
        mouseDrag = mouseInput.GetScreenPosition() - transform.position;
        return mouseDrag.magnitude;
    }

    private float GetDragAngle()
    {
        mouseDrag = mouseInput.GetScreenPosition() - transform.position;
        return Vector3.Angle(transform.rotation * Vector3.down, mouseDrag.normalized);
    }

    private void ActivateGravityBeam()
    {
        isMoving = false;
        gravityBeamActivated = true;
        gravityBeam.isActive = true;

        snapshot snapshot = GameObject.Find("phone_button_32").GetComponent<snapshot>();
        snapshot.renderer.enabled = true;
    }
}