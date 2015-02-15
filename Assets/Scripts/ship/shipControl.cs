using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour
{
    private bool hold = false;
    private float dot;
    private Vector3 storedMousePosition;
    private Vector3 mouseDrag;

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
        storedMousePosition = transform.position;
    }

    void OnMouseDrag()
    {
        hold = true;
    }
    
    void OnMouseUp()
    {
        // Calculate the drag manually and activate beam if in angle
        if (GetDragAngle() < beamActivateAngle)
        {
            ActivateGravityBeam();
        } else
        {
            hold = false;
        }
    }

    // Private functions ------------------------------------------------------------------

    private Vector3 GetInputPosition()
    {
        Vector3 camPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        camPosition.z = 0;
        return camPosition;
    }

    private float GetDragAngle()
    {
        mouseDrag = GetInputPosition() - storedMousePosition;
        dot = Vector3.Dot(transform.position.normalized, mouseDrag.normalized);
        return ConvertDotToAngle(dot);
    }

    private float ConvertDotToAngle(float nb)
    {
        // changes scale from -1:1 to 180:0
        return (-nb / 2f + 0.5f) * 180f;
    }

    private void ActivateGravityBeam()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        print ("ActivateGravityBeam");
        gravityBeam.isActive = true;
        hold = true;
        
        snapshot snapshot = GameObject.Find("phone_button_32").GetComponent<snapshot>();
        snapshot.renderer.enabled = true;
    }
}