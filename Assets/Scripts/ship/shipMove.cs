using UnityEngine;
using System.Collections;

// Class to deal with moving the ship to a certain angle on the orbit

public class shipMove : MonoBehaviour
{
    // private vars
    float angle;
    float tarAngle;
    float moveAngle;
    float accAngle;
    float decAngle;
    float percAngle;
    float speed;
    float deacceleration;

    tinker tinker;

    // tinkered vars
    float acceleration;

    // public vars

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
    }
    
    void Update()
    {
        UpdateTinker();
        MoveShip();
    }
    
    void UpdateTinker()
    {
        acceleration = tinker.shipAcceleration;
    }

    // public functions ----------------------------------------------------

    public void SetNewPosition(Vector3 newPosition)
    {
        // new position should be local to the sun
        tarAngle = GetSignedAngle(transform.localPosition, newPosition);
    }

    // private functions ---------------------------------------------------

    private void MoveShip()
    { 
        if (tarAngle != 0)
        {
            moveAngle = GetMoveAngle();
            transform.localPosition = Quaternion.Euler(0, 0, moveAngle) * transform.localPosition;
            tarAngle -= moveAngle;
        }
    }
        
    private float GetMoveAngle()
    {
        deacceleration = 2*Mathf.Sqrt(acceleration);
        speed = (acceleration * tarAngle) - deacceleration;
        return speed * Time.deltaTime;
    }

    private float GetSignedAngle(Vector3 from, Vector3 to)
    {
        angle = Vector3.Angle(from, to);
        angle = angle * Mathf.Sign(Vector3.Cross(from, to).z);
        return angle;
    }


}