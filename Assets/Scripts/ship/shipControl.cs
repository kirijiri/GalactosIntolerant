using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour
{
    private float dot;
    private Vector3 storedPosition;
    private Vector3 mouseDrag;
    public bool isMoving = true;
    public bool gravityBeamActivated = false;

    // tinkered
    private tinker tinker;
    private float beamActivateAngle;

    // gravity beam
    private gravityBeam gravityBeam;
    private shipAnimation shipAnim;

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
    }
    
    // events -------------------------------------------------------------

    void OnMouseDown()
    {
        storedPosition = transform.position;
        isMoving = false;
        shipAnim.Open();
    }

    void OnMouseDrag()
    {
        isMoving = false;
        if (GetDragDistance() > 0.15)
        {
            // NOTHING YET
        }
    }
    
    void OnMouseUp()
    {
        // Calculate the drag manually and activate beam if in angle
        if (GetDragAngle() < beamActivateAngle)
        {
            ActivateGravityBeam();
        } else
        {
            shipAnim.Close();
            isMoving = true;
        }
    }

    // Private functions ------------------------------------------------------------------

    private float GetDragDistance()
    {
        mouseDrag = mouseInput.GetScreenPosition() - storedPosition;
        return mouseDrag.magnitude;
    }

    private float GetDragAngle()
    {
        mouseDrag = mouseInput.GetScreenPosition() - storedPosition;
        dot = Vector3.Dot(transform.localPosition.normalized, mouseDrag.normalized);
        return ConvertDotToAngle(dot);
    }

    private float ConvertDotToAngle(float nb)
    {
        // changes scale from -1:1 to 180:0
        return (nb / 2f + 0.5f) * 180f;
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