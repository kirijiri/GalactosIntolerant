using UnityEngine;
using System.Collections;

public class planetSound : MonoBehaviour {
    public AudioClip flicked;
    public AudioClip held;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // PUBLIC -----------------------------------------------------------------

    public void AudioFlick()
    {
        //should be playing audio
        audio.Play();
    }

    public void AudioHold()
    {

    }

}
