using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class shipSound : MonoBehaviour
{
    public AudioClip thrusterNormal;
    public AudioClip thrusterEmergency;
    public AudioClip beamOffToCharge;
    public AudioClip beamCharge;
    public AudioClip beamChargeToFiring;
    public AudioClip beamFiring;
    public AudioClip beamFiringToOff;
    private float thrusterCooldownTimer;

    void Start()
    {
        thrusterCooldownTimer = Time.time;
    }

    public IEnumerator AudioThrusterNormal(float accel)
    {
        print ("here" + accel);

        audio.clip = thrusterNormal;
        audio.Play();
        yield return new WaitForSeconds (audio.clip.length);

        //if (Time.time - thrusterCooldownTimer < 2) return;
        //audio.Stop();
        //audio.PlayOneShot(thrusterNormal);
        //thrusterCooldownTimer = Time.time;
    }
    
    public void AudioThrusterEmergency()
    {
        audio.Stop();
        audio.PlayOneShot(thrusterEmergency);
    }
}
