using UnityEngine;
using System;
using System.Collections;

public class shipOrbitControl : MonoBehaviour
{
    private Animator anim;
    private GameObject ship;
    private GameObject sun;
    private shipControl shipCtrl;
    private float inputRadius;
    private float upper;
    private float lower;
    private Vector3 centre;
    private Vector3 curPos;
    private Vector3 mousePosition;
    private Vector3 solarPosition;
    private tinker tinker;

    // tinkered
    float orbitRadius;
    float lowerBoundary = 20;
    float upperBoundary = 80;

    void Start()
    {
        sun = GameObject.Find("sun");
        ship = GameObject.Find("ship");
        shipCtrl = ship.GetComponent<shipControl>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        centre = sun.transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateTinker();

        mousePosition = mouseInput.GetScreenPosition();
        solarPosition = ConvertToSolarPosition(mousePosition);
        inputRadius = ConvertSolarPosToRadius(solarPosition);
        
        upper = orbitRadius + upperBoundary;
        lower = orbitRadius - lowerBoundary;
        
        if (inputRadius >= lower && inputRadius <= upper)
        {
            anim.SetBool("hover", shipCtrl.isMoving);
            if (Input.GetMouseButtonDown(0))
            { 
                if (shipCtrl.isMoving)
                {
                    ship.SendMessage("SetNewPosition", solarPosition);
                }
            }
        } else
        {
            anim.SetBool("hover", false);
        }
    }

    void UpdateTinker()
    {
        orbitRadius = tinker.shipOrbitRadius;
        lowerBoundary = tinker.shipClickLowerBoundary;
        upperBoundary = tinker.shipClickUpperBoundary;
    }

    // private methods

    private Vector3 ConvertToSolarPosition(Vector3 position)
    {
        return position - centre;
    }

    private float ConvertSolarPosToRadius(Vector3 position)
    {
        // based on the pixel to unit scale our sprites are using
        return position.magnitude * 200; 
    }
}