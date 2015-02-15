using UnityEngine;
using System.Collections;

public class planetGravityPull : MonoBehaviour
{
    public bool isAligned = false;

    // from settings
    private float gbEffectThreshold;
    private float gbAlignmentThreshold;
    
    // private
    private GameObject sun;
    private GameObject ship;
    private GameObject planetGraphic;
    private Vector3 beamVec;
    private Ray beam;
    private float beamDist;
    private Vector3 beamIntersectPoint;
    private bool gravityBeamActive;
    
    // settings objects
    private gravityBeam gravityBeam;
    private tinker tinker;
    private debug debugScript;
    

    //-------------------------------------------------------------------
    
    void Start()
    {
        sun = GameObject.Find("sun");
        ship = GameObject.Find("ship");

        // sprite renderer
        planetGraphic = GetComponent<planetInit>().planetGraphic;

        // gravity beam
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();

        // settings
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        // debug
        debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTinker();

        // Look for gravity beam
        gravityBeamActive = gravityBeam.enabled;

        // get beam 
        beamVec = sun.transform.position - ship.transform.position;
        beamVec.z = 0;
        beam = new Ray(ship.transform.position, beamVec * 2);

        if (debugScript.drawShipDiameter)
        {
            Debug.DrawRay(ship.transform.position, beamVec * 2, Color.white, 3.0f, false);
        }

        // get intersection point and distance to the beam ray
        beamDist = DistanceToRay(beam, transform.position);
        beamIntersectPoint = IntersectionWithRay(beam, transform.position);
        //Debug.DrawLine(transform.position, beamIntersectPoint, Color.red, 3.0f, false);

        // colour the planets when in the threshold and trigger pull
        if (beamDist < gbEffectThreshold && gravityBeamActive)
        {
            color.red(planetGraphic);
            gravityPull(beam, beamIntersectPoint);

            if (beamDist < gbAlignmentThreshold)
            {
                color.blue(planetGraphic);
                isAligned = true;
            }
        } else
        {
            color.white(planetGraphic);
        }
    }

    void UpdateTinker()
    {
        gbEffectThreshold = tinker.GBEffectThreshold;
        gbAlignmentThreshold = tinker.GBAlignmentThreshold;
    }

    //------------------------------------------------------------------- helper functions

    private void gravityPull(Ray beam, Vector3 intersect)
    {
        rigidbody2D.velocity = intersect - transform.position;
    }

    private static float DistanceToRay(Ray ray, Vector3 point)
    {
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }
    
    private static Vector3 IntersectionWithRay(Ray ray, Vector3 point)
    {
        return ray.origin + ray.direction * Vector3.Dot(ray.direction, point - ray.origin);
    }
}
