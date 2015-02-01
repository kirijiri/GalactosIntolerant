﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class planetStats : MonoBehaviour {
	public float initSpeed = 0;
	public float orbitRadius = 0;
	public float population = 0;
	public float followers = 0;
	public string name = "";
	public int size = 14;
	public string biome = "";
	public string race = "";
	public string look = "";
	public string note = "";
	public string[] idleMessages;
	public string[] minorMessages;
	public string[] majorMessages;
	List<int> idleTrack = new List<int>();
	List<int> minorTrack = new List<int>();
	List<int> majorTrack = new List<int>();
	
	void Start () {
		print (idleMessages.Length);
		for (int i = 0; i < idleMessages.Length; i++) idleTrack.Add(i);
		for (int i = 0; i < minorMessages.Length; i++) minorTrack.Add(i);;
		for (int i = 0; i < majorMessages.Length; i++) majorTrack.Add(i);
	}

	public string GetIdleMessage () {
		/*
		print (idleTrack.Count);
		int rand = Random.Range(0, idleTrack.Count);
		Debug.Log(rand);
		idleTrack.RemoveAt(rand);
		*/
		return "hi";
	}
}