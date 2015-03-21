using UnityEngine;
using System.Collections;

public class thrustAnimation : MonoBehaviour
{
    private Animator anim;
    private float currAccel;
    private float avgAccel;
    private bool clockwise;
    private Vector3 currVelocity;
    private Vector3 lastVelocity;
    private Vector3 lastPos;
    private Vector3 right;
    private Queue accelQueue = new Queue();

    // tinkered 
    private tinker tinker;
    public int queueSize;
    public float accelScale;
    
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        lastPos = transform.position;
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
    }

    void Update()
    {
        UpdateTinker();

        // calculate the change in velocity
        currVelocity = transform.position - lastPos;
        currAccel = Mathf.Abs( currVelocity.magnitude - lastVelocity.magnitude );

        // work out direction of acceleration
        if (currAccel > 0.0f)
        {
            right = transform.rotation * Vector3.right;
            clockwise = Vector3.Dot(currVelocity.normalized, right) > 0;

            if (!clockwise)
            {
                currAccel *= -1;
            }
        }

        // add the acceleration to the queue
        accelQueue.Enqueue(currAccel * accelScale);
        if (accelQueue.Count > queueSize)
        {
            accelQueue.Dequeue ();
        }

        // calulate average in queue
        avgAccel = 0.0f;
        foreach(float a in accelQueue)
        {
            avgAccel += a;
        }
        avgAccel /= accelQueue.Count;

        // set in animator
        anim.SetFloat("accel", avgAccel);

        // store data for next check
        lastPos = transform.position;
        lastVelocity = currVelocity;
    }

    void UpdateTinker()
    {
        queueSize = Mathf.Max(1, tinker.thrusterSmooth);
        accelScale = tinker.thrusterAccelScale;
    }
}
