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
    private float diffAcceleration;
    private Vector3 lastPos;
    private Vector3 right;
    private Queue accelQueue = new Queue();
    public int queueSize = 20;
    public float accelScale = 1000;
    
    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        lastPos = transform.position;
    }

    void Update()
    {
        // calculate the change in velocity
        currVelocity = transform.position - lastPos;
        diffAcceleration = Mathf.Abs( currVelocity.magnitude - lastVelocity.magnitude );

        // set it in the animator
        if (diffAcceleration > 0.0f)
        {

            right = transform.rotation * Vector3.right;
            clockwise = Vector3.Dot(currVelocity.normalized, right) > 0;

            currAccel = diffAcceleration;
            if (!clockwise)
            {
                currAccel *= -1;
            }
        } else
        {
            currAccel = 0.0f;
        }

        // add the acceleration to the queue
        accelQueue.Enqueue(currAccel * accelScale);
        if (accelQueue.Count > queueSize)
        {
            accelQueue.Dequeue ();
        }

        string printStr = "";
        foreach (float a in accelQueue)
        {
            printStr += a.ToString() + ", ";
        }

        // calulate average in queue
        avgAccel = 0.0f;
        foreach(float a in accelQueue)
        {
            avgAccel += a;
        }
        avgAccel /= accelQueue.Count;
        print(avgAccel);


        // store data for next check
        lastPos = transform.position;
        lastVelocity = currVelocity;
    }
}
