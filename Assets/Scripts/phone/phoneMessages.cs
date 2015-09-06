using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class phoneMessages : MonoBehaviour
{
    private GameObject[] planets;
    private float timer = 0;
    private List<int> planetsTrack;
    private string message;
    private float idleTimer;
    private phoneAnimation phoneAnimation;
    private tinker tinker;

    void Start()
    {
        phoneAnimation = GetComponent<phoneAnimation>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        
        planets = GameObject.FindGameObjectsWithTag("Planet");
        ResetPlanetTrack();

		StartCoroutine(UpdateIdleMessages());
    }

    void ResetPlanetTrack()
    {
        planetsTrack = new List<int>();
        for (int i = 0; i < planets.Length; i++)
            planetsTrack.Add(i);
    }

	IEnumerator UpdateIdleMessages ()
	{
		// get a random planet index that hasn't been used yet (if it runs out, fill up the list again)
		// TODO: figure out how many messages are in each planet, the planets might not have to change,
		// just the messages
		if (planetsTrack.Count == 0)
			ResetPlanetTrack();
		int rand = Random.Range(0, planetsTrack.Count);
		
		// set idle message text
		message = planets [rand].GetComponent<planetMessaging>().GetIdleMessage();
		if (message == "")
			StartCoroutine( UpdateIdleMessages() );
		
		// play animation
		print (message);
		//phoneAnimation.AddNewMessage(message);
		
		// remove planet from track list to not repeat it immidiately again
		planetsTrack.RemoveAt(rand);

		yield return new WaitForSeconds(2);
		StartCoroutine( UpdateIdleMessages() );
	}
    
    void Update()
    {
		/*
        idleTimer = tinker.idleTimer;

        // show idle message from random planet every couple of seconds
        timer += Time.deltaTime;
        if (phoneAnimation.numMessages < tinker.initialMessageCount || timer > idleTimer)
        {
            timer = 0;

            // get a random planet index that hasn't been used yet (if it runs out, fill up the list again)
            // TODO: figure out how many messages are in each planet, the planets might not have to change,
            // just the messages
            if (planetsTrack.Count == 0)
                ResetPlanetTrack();
            int rand = Random.Range(0, planetsTrack.Count);

            // set idle message text
            message = planets [rand].GetComponent<planetMessaging>().GetIdleMessage();
            if (message == "")
                return;

            // play animation
            phoneAnimation.AddNewMessage(message);

            // remove planet from track list to not repeat it immidiately again
            planetsTrack.RemoveAt(rand);
        }
        */
    }

    public void SendNewMessage(GameObject planet)
    {
        // set idle message text
        message = planet.GetComponent<planetMessaging>().GetDamageMessage();
        if (message == "")
            return;

        // play animation
        phoneAnimation.AddNewMessage(message);
    }
}
