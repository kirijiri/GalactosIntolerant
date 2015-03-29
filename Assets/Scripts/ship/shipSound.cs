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

    private bool play_thruster_normal = false;
    private bool isMoving;

    private enum Fade {In, Out};
    private float fadeTime = 2.0f;
    private float defaultVolume;

    public void Start()
    {
        defaultVolume = audio.volume;
    }
    public void AudioThrusterNormal(float accel)
    {
        isMoving = (accel > 0.05f || accel < -0.05f);
        if (!play_thruster_normal && isMoving)
        {
            StartCoroutine(PlayAudioThrusterNormal());
            play_thruster_normal = true;
        }
        if (play_thruster_normal && !isMoving)
        {
            StopAudioThrusterNormal();
        }
    }

    private IEnumerator PlayAudioThrusterNormal()
    {
        audio.clip = thrusterNormal;
        audio.volume = defaultVolume;

        audio.Play();
        yield return new WaitForSeconds (audio.clip.length);
        play_thruster_normal = false;
    }

    private void StopAudioThrusterNormal()
    {
        play_thruster_normal = false;
        StartCoroutine(FadeAudio(fadeTime, Fade.Out));
        print ("force stop fade");
        //audio.clip = thrusterNormal;
        //audio.Stop();
    }
    
    public void AudioThrusterEmergency()
    {
        audio.Stop();
        audio.PlayOneShot(thrusterEmergency);
    }

    private IEnumerator FadeAudio (float timer, Fade fadeType) 
    {
        float start = fadeType == Fade.In? 0.0F : 1.0F;
        float end = fadeType == Fade.In? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F/timer;
        
        while (i <= 1.0F) {
            i += step * Time.deltaTime;
            audio.volume = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(step * Time.deltaTime);
        }
    }
}
