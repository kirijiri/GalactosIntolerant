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

    private phoneAnimation phoneAnimation;
    private tinker tinker;

    void Start()
    {
        phoneAnimation = GameObject.Find("messages").GetComponent<phoneAnimation>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        GetComponent<Text>().text = "Oh hello";
        planets = GameObject.FindGameObjectsWithTag("Planet");

        ResetPlanetTrack();
    }

    void ResetPlanetTrack()
    {
        planetsTrack = new List<int>();
        for (int i = 0; i < planets.Length; i++)
            planetsTrack.Add(i);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > tinker.idleTimer)
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
            GetComponent<Text>().text = message;

            // play animation
            phoneAnimation.AddNewMessage(message);

            // remove planet from track list to not repeat it immidiately again
            planetsTrack.RemoveAt(rand);
        }
    }
}
