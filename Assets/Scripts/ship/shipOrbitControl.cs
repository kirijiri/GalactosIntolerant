using UnityEngine;
using System;
using System.Collections;

public class shipOrbitControl : MonoBehaviour
{
    private GameObject ship;
    private GameObject sun;
    private float clickedRadius;
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
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        centre = sun.transform.position;
    }

    void Update()
    {
        UpdateTinker();

        if (Input.GetMouseButtonDown(0))
        { 
            mousePosition = mouseInput.GetScreenPosition();
            solarPosition = ConvertToSolarPosition(mousePosition);
            clickedRadius = ConvertSolarPosToRadius(solarPosition);

            upper = orbitRadius + upperBoundary;
            lower = orbitRadius - lowerBoundary;

            if (clickedRadius >= lower && clickedRadius <= upper)
            {
                ship.SendMessage("SetNewPosition", solarPosition);
            }
        }
    }

    void UpdateTinker()
    {
        orbitRadius = tinker.shipOrbitRadius;
        lowerBoundary = tinker.shipClickLowerBoundary;
        upperBoundary = tinker.shipClickUpperBoundary;
    }

    // private methods

    private Vector3 ConvertToSolarPosition( Vector3 position )
    {
        return position - centre;
    }

    private float ConvertSolarPosToRadius( Vector3 position )
    {
        // based on the pixel to unit scale our sprites are using
        return position.magnitude * 200; 
    }
}