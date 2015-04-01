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
			audio.Stop ();
			audio.PlayOneShot (damaged);
		}
	}
}
