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
    public AudioClip beamFiringToOff;
    public AudioClip beamChargeToOff;

    private AudioSource thusterNormalSource;
    private AudioSource thusterEmergencySource;

    private bool playing_thruster_normal = false;
    private bool playing_thruster_emergency = false;
    private bool isMoving;

    private enum Fade {In, Out};
    private float fadeTime = 2.0f;
    private float defaultVolume;

    public void Start()
    {
        defaultVolume = audio.volume;

        thusterNormalSource = (AudioSource) gameObject.AddComponent("AudioSource");
        thusterNormalSource.clip = thrusterNormal;
        thusterNormalSource.playOnAwake = false;
        
        thusterEmergencySource = (AudioSource) gameObject.AddComponent("AudioSource");
        thusterEmergencySource.clip = thrusterEmergency;
        thusterEmergencySource.playOnAwake = false;
    }

// thruster normal -------------------------------------------------------------

    // gets called every frame
    public void AudioThrusterNormal(float accel)
    {
        isMoving = (accel > 0.08f || accel < -0.08f);
        if (!playing_thruster_normal && isMoving)
        {
            playing_thruster_normal = true;
            StartCoroutine(PlayAudioThrusterNormal());
        }
        if (playing_thruster_normal && !isMoving)
        {
            playing_thruster_normal = false;
            StopAudioThrusterNormal();
        }
    }

    private IEnumerator PlayAudioThrusterNormal()
    {
        thusterNormalSource.Stop();
        thusterNormalSource.volume = defaultVolume;
        thusterNormalSource.Play();
        yield return new WaitForSeconds (thusterNormalSource.clip.length);

        thusterNormalSource.Stop();
        playing_thruster_normal = false;
    }

    private void StopAudioThrusterNormal()
    {
        StartCoroutine(FadeAudio(thusterNormalSource, fadeTime, Fade.Out));
    }

// thruster emergency -------------------------------------------------------------

    // gets called every frame
    public void AudioThrusterEmergency()
    {
        if (playing_thruster_normal && !playing_thruster_emergency)
        {
            playing_thruster_normal = false;
            playing_thruster_emergency = true;
            PlayAudioThrusterEmergency();
        }
    }

    public void PlayAudioThrusterEmergency()
    {
        thusterEmergencySource.Stop();
        thusterEmergencySource.Play();
        playing_thruster_emergency = false;
    }

    private IEnumerator FadeAudio (AudioSource source, float timer, Fade fadeType) 
    {
        float start = fadeType == Fade.In? 0.0F : 1.0F;
        float end = fadeType == Fade.In? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F/timer;
        
        while (i <= 1.0F) {
            i += step * Time.deltaTime;
            source.volume = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(step * Time.deltaTime);
        }
        source.Stop();
    }
}
