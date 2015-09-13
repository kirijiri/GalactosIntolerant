using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class planetSound : MonoBehaviour 
{
    public AudioClip flicked;
    public AudioClip held;
    public AudioClip flickedDamaged;
    public AudioClip heldDamaged;

    private AudioClip curFlicked;
    private AudioClip curHeld;
    
    private enum Fade {In, Out};
    private float fadeTime = 1.0f;
    private float defaultVolume;

    private void Start()
    {
        curFlicked = flicked;
        curHeld = held;
        defaultVolume = GetComponent<AudioSource>().volume;
    }

    public void AudioFlick()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().volume = defaultVolume;
        GetComponent<AudioSource>().PlayOneShot(flicked);
    }

    public void AudioHold()
    {
		GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().volume = defaultVolume;
		GetComponent<AudioSource>().PlayOneShot(held);
    }

    public void AudioHoldRelease()
    {
        StartCoroutine(FadeAudio(GetComponent<AudioSource>(), fadeTime, Fade.Out));
    }

    public void AudioToDamageSounds()
    {
        curFlicked = flickedDamaged;
        curHeld = heldDamaged;
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
