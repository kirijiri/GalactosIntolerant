using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {
	public int aligned_planet_count = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
}
