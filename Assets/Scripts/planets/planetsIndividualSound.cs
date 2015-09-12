using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class planetsIndividualSound : MonoBehaviour
{
	public AudioClip damaged;
	private bool playOnce = true;

	public void AudioDamaged ()
	{
		if (playOnce)
		{
			playOnce = false;
			GetComponent<AudioSource>().Stop ();
			GetComponent<AudioSource>().PlayOneShot (damaged);
		}
	}
}
