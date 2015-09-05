using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class snapshotSound : MonoBehaviour
{
    public AudioClip snapshot;
    private static snapshotSound instance = null;

    public static snapshotSound Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void AudioSnapshot()
    {
        //audio.clip = snapshot;
        //audio.Play();
        //audio.PlayOneShot(snapshot);
        StartCoroutine(PlayAudioSnapshot());
    }

    private IEnumerator PlayAudioSnapshot()
    {
        GetComponent<AudioSource>().clip = snapshot;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
    }
}

