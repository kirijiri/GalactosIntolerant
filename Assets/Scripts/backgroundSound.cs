using UnityEngine;
using System.Collections;

public class backgroundSound : MonoBehaviour 
{
    public AudioClip backgroundDamaged;
    
    public void AudioToDamageSounds()
    {
        audio.Stop();
        audio.clip = backgroundDamaged;
        audio.Play();
    }
}
