using UnityEngine;
using System.Collections;

// Class to deal with moving the ship to a certain angle on the orbit

public class shipMove : MonoBehaviour
{
    // private vars
    private float angle;
    private float tarAngle;
    private float moveAngle;
    private float accAngle;
    private float decAngle;
    private float percAngle;
    private float speed;
    private float deceleration;
    private tinker tinker;
    private shipControl shipCtrl;
    private thrustAnimation thrusters;

    // tinkered vars
    float accelerationRate;
    float decelerationRate;
    float restThreshold;

        

    // public vars

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        thrusters = GameObject.Find("thrusters").GetComponent<thrustAnimation>();
        shipCtrl = GameObject.Find("ship").GetComponent<shipControl>();
    }
    
    void Update()
    {
        UpdateTinker();
        MoveShip();
    }
    
    void UpdateTinker()
    {
        accelerationRate = tinker.shipAcceleration;
        decelerationRate = tinker.shipDeceleration;
        restThreshold = tinker.shipAnimRestThreshold;
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
        if (shipCtrl.isMoving && tarAngle != 0)
        {
            if (shipCtrl.gravityBeamActivated)
            {
                return;
            }

            if (tarAngle > restThreshold)
            {
				thrusters.TAnimAntiClockWiseHigh();
			} else if (tarAngle < -restThreshold)
            {
				thrusters.TAnimClockWiseHigh();
			} else
            {
				thrusters.TAnimDefault();
			}

			
            moveAngle = GetMoveAngle();
            transform.localPosition = Quaternion.Euler(0, 0, moveAngle) * transform.localPosition;
            tarAngle -= moveAngle;
        }
    }
        
    private float GetMoveAngle()
    {
        deceleration = 2 * Mathf.Sqrt(decelerationRate);
        speed = (accelerationRate * tarAngle) - deceleration;
        return speed * Time.deltaTime;
    }

    private float GetSignedAngle(Vector3 from, Vector3 to)
    {
        angle = Vector3.Angle(from, to);
        angle = angle * Mathf.Sign(Vector3.Cross(from, to).z);
        return angle;
    }


}