using UnityEngine;
using System.Collections;

public class backgroundSound : MonoBehaviour 
{
    public AudioClip backgroundDamaged;
    
    public void AudioToDamageSounds()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = backgroundDamaged;
        GetComponent<AudioSource>().Play();
    }
}
