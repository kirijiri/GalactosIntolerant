using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour
{
    private float dot;
    private Vector3 storedPosition;
    private Vector3 mouseDrag;
    public bool isMoving = true;

    // tinkered
    private tinker tinker;
    private float beamActivateAngle;

    // gravity beam
    private gravityBeam gravityBeam;

    //-------------------------------------------------------------------
    
    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
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
    }

    void OnMouseDrag()
    {
        isMoving = false;
    }
    
    void OnMouseUp()
    {
        // Calculate the drag manually and activate beam if in angle
        if (GetDragAngle() < beamActivateAngle)
        {
            ActivateGravityBeam();
        } else
        {
            isMoving = true;
        }
    }

    // Private functions ------------------------------------------------------------------

    private float GetDragAngle()
    {
        mouseDrag = mouseInput.GetScreenPosition() - storedPosition;
        dot = Vector3.Dot(transform.position.normalized, mouseDrag.normalized);
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

        shipLook.beamOn( gameObject );
        gravityBeam.isActive = true;

        snapshot snapshot = GameObject.Find("phone_button_32").GetComponent<snapshot>();
        snapshot.renderer.enabled = true;
    }
}