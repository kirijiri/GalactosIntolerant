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
    private Vector3 planetDist;
    private planetAnimation animation;
    
    // settings objects
    private gravityBeam gravityBeam;
    private tinker tinker;
    private debug debugScript;
    

    //-------------------------------------------------------------------
    
    void Start()
    {
        sun = GameObject.Find("sun");
        ship = GameObject.Find("ship");

        // gravity beam
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
        animation = GetComponent<planetAnimation>();

        // settings
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        // debug
        debugScript = (debug)GameObject.Find("debug").GetComponent(typeof(debug));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTinker();

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
        isAligned = false;
        planetLook.standard(gameObject);
        if (beamDist < gbEffectThreshold)
        {
            // stop gravity beam at sun
            planetDist = transform.position - ship.transform.position;
            if (beamVec.magnitude <= planetDist.magnitude)
                return;

            if (gravityBeam.isActive && gravityBeam.available)
            {
                planetLook.gravity(gameObject);
                gravityPull(beam, beamIntersectPoint);
            }

            if (beamDist < gbAlignmentThreshold)
            {
                planetLook.aligned(gameObject);
                isAligned = true;
            }
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
