using UnityEngine;
using System.Collections;

// Class that deals with all the player inputs to the ship

public class shipControl : MonoBehaviour
{
    tinker tinker;
    bool hold = false;
    float dot;
    Vector3 storedMousePosition;
    Vector3 mouseDrag;

    // tinkered
    float beamActivateAngle;

    // get rid of these eventually!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public float gravityBeamThreshold = 0.3f;
    public float alignmentThreshold = 0.00005f;
    public bool gravityBeamEngaged = false;
    
    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
    }

    void Update()
    {
        UpdateTinker();
    }
    
    void UpdateTinker()
    {
        beamActivateAngle = tinker.beamActivateAngle;
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
        // Calculate the drag manually and activcate beam if in angle
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
        gravityBeamEngaged = true;
        hold = true;
        
        snapshot snapshot = GameObject.Find("phone_button_32").GetComponent<snapshot>();
        snapshot.renderer.enabled = true;
    }
}