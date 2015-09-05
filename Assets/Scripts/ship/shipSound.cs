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
    
    public float thrusterNormalVolume = 1.0f;
    public float thrusterEmergencyVolume = 1.0f;
    public float beamOffToChargeVolume = 1.0f;
    public float beamChargeVolume = 1.0f;
    public float beamChargeToFiringVolume = 1.0f;
    public float beamFiringToOffVolume = 1.0f;
    public float beamChargeToOffVolume = 1.0f;

    private AudioSource thusterNormalSource;
    private AudioSource thusterEmergencySource;
    private AudioSource beamSource;
    private AudioSource beamChargeSource;
    private AudioSource beamFiringToOffSource;

    private bool playing_thruster_normal = false;
    private bool playing_thruster_emergency = false;
    private bool playing_beam_charge = false;
    private bool playing_beam_fire = false;
    private bool isMoving;

    private enum Fade {In, Out};
    private float fadeTime = 2.0f;

    public void Start()
    {
        thusterNormalSource = (AudioSource) gameObject.AddComponent<AudioSource>();
        thusterNormalSource.clip = thrusterNormal;
        thusterNormalSource.playOnAwake = false;
        
        thusterEmergencySource = (AudioSource) gameObject.AddComponent<AudioSource>();
        thusterEmergencySource.clip = thrusterEmergency;
        thusterEmergencySource.playOnAwake = false;

        beamSource = (AudioSource) gameObject.AddComponent<AudioSource>();
        beamSource.playOnAwake = false;
        
        beamChargeSource = (AudioSource) gameObject.AddComponent<AudioSource>();
        beamChargeSource.clip = beamCharge;
        beamChargeSource.loop = true;
        beamChargeSource.playOnAwake = false;
        
        beamFiringToOffSource = (AudioSource) gameObject.AddComponent<AudioSource>();
        beamFiringToOffSource.clip = beamFiringToOff;
        beamFiringToOffSource.loop = false;
        beamFiringToOffSource.playOnAwake = false;
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
        thusterNormalSource.volume = thrusterNormalVolume;
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

// beam -/////////////------------------------------------------------------------

    public void AudioBeamOpen()
    {
        playing_beam_charge = true;
        if (!playing_beam_fire)
        {
            StartCoroutine(PlayAudioBeamOpen());
        }
    }
    
    private IEnumerator PlayAudioBeamOpen()
    {
        beamSource.Stop();
        beamSource.clip = beamOffToCharge;
        beamSource.volume = beamOffToChargeVolume;
        beamSource.Play();
        yield return new WaitForSeconds (beamSource.clip.length-0.1f); //don't wait until the end, causes sound gap

        if (playing_beam_charge)
        {
            beamChargeSource.volume = beamChargeVolume;
            beamChargeSource.Play();
        }
    }

    public void AudioBeamCancel()
    {
        playing_beam_charge = false;
        if (!playing_beam_fire)
        {
            StartCoroutine(FadeAudio(beamSource, 0.5f, Fade.Out));
            StartCoroutine(FadeAudio(beamChargeSource, 0.5f, Fade.Out));
            GetComponent<AudioSource>().volume = beamChargeToOffVolume;
            GetComponent<AudioSource>().PlayOneShot(beamChargeToOff);
        }
    }

    public void AudioBeamFire()
    {
        if (!playing_beam_fire)
        {
            playing_beam_fire = true;
            playing_beam_charge = false;
            StartCoroutine(FadeAudio(beamChargeSource, 0.5f, Fade.Out));
            StartCoroutine(PlayAudioBeamFire());
        }
    }

    private IEnumerator PlayAudioBeamFire()
    {
        beamSource.Stop();
        beamSource.clip = beamChargeToFiring;
        beamSource.volume = beamChargeToFiringVolume;
        beamSource.Play();
        yield return new WaitForSeconds (beamSource.clip.length-0.1f); //don't wait until the end, causes sound gap

        /*
        // maybe put back in if the Fire sound and die sound gets split
        beamFiringToOffSource.Stop();
        beamFiringToOffSource.volume = beamFiringToOffVolume;
        beamFiringToOffSource.Play();
        yield return new WaitForSeconds (beamFiringToOffSource.clip.length); 
        */
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
