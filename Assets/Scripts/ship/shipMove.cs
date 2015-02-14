using UnityEngine;
using System.Collections;

// Class to deal with moving the ship to a certain angle on the orbit

public class shipMove : MonoBehaviour
{
    // private vars
    float angle;
    float tarAngle;
    float fullMoveAngle;
    float moveAngle;
    float accAngle;
    float decAngle;
    float percAngle;
    float speedPercent;
    float speed;

    bool movePositive;

    tinker tinker;

    // tinkered vars
    float maxSpeed;
    float accPercent;
    float decPercent;

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
        maxSpeed = tinker.shipMaxSpeed;
        accPercent = tinker.shipAccPercent;
        decPercent = tinker.shipDecPercent;
    }

    // public functions ----------------------------------------------------

    public void SetNewPosition(Vector3 newPosition)
    {
        // new position should be local to the sun
        tarAngle = GetSignedAngle(transform.localPosition, newPosition);
        fullMoveAngle = tarAngle;
        movePositive = tarAngle > 0;
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
        speedPercent = Mathf.Max(5, GetSpeedPercent());
        speed = maxSpeed //* (speedPercent / 100);

        // never move more then the leftover angle
        if (movePositive){
            return Mathf.Min (speed * Time.deltaTime, tarAngle);
        }
        else{
            return Mathf.Max (-speed * Time.deltaTime, tarAngle);
        }
    }

    private float GetSpeedPercent()
    {
        percAngle = GetPercentage(tarAngle, 0, fullMoveAngle, true);
        if (percAngle < accPercent)
        {
            return GetPercentage(percAngle, 0, accPercent, false);
        } else if (percAngle > decPercent)
        {
            return GetPercentage(percAngle, decPercent, 100, true);
        } else
        {
            return 100;
        }
    }

    private float GetPercentage(float value, float min, float max, bool invert)
    {
        value -= min;
        max -= min;

        if (invert)
        {
            return 100 - (value / (max / 100));
        } else
        {
            return value / (max / 100);
        }
    }

    private float GetSignedAngle(Vector3 from, Vector3 to)
    {
        angle = Vector3.Angle(from, to);
        angle = angle * Mathf.Sign(Vector3.Cross(from, to).z);
        return angle;
    }


}