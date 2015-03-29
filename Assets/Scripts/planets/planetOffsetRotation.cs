using UnityEngine;
using System.Collections;

public class planetOffsetRotation : MonoBehaviour {
    GameObject parent;

	// Use this for initialization
	void Start () 
    {
        parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.localRotation = Quaternion.Inverse(parent.transform.rotation);
	}
}
