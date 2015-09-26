using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class phoneMessages : MonoBehaviour
{
    private GameObject[] planets;
    private float timer = 0;
    private List<GameObject> planetsTrack;
    private Texture2D message;
    private phoneSetup phone;
    private tinker tinker;
    private int _idleTimer = 6;
    private float criticalPopulationPercentage = 50.0f;

    void Start()
    {
        phone = GameObject.Find("phone_setup").GetComponent<phoneSetup>();
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        
        planets = GameObject.FindGameObjectsWithTag("Planet");
        ResetPlanetTrack();

        StartCoroutine(UpdateIdleMessages());
    }

    void ResetPlanetTrack()
    {
        planetsTrack = new List<GameObject>();
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<planetSettings>().population <= (planet.GetComponent<planetSettings>().maxPopulation / 100 * criticalPopulationPercentage)) continue;
            planetsTrack.Add(planet);
        }
    }

    void CheckPlanetTrack()
    {
        foreach (GameObject planet in planetsTrack)
        {
            if (planet.GetComponent<planetSettings>().population <= (planet.GetComponent<planetSettings>().maxPopulation / 100 * criticalPopulationPercentage)) continue;
            {
                ResetPlanetTrack();
                break;
            }
        }
    }

    IEnumerator UpdateIdleMessages()
    {
        yield return new WaitForSeconds(_idleTimer);

        CheckPlanetTrack();

        // get a random planet index that hasn't been used yet (if it runs out, fill up the list again)
        // TODO: figure out how many messages are in each planet, the planets might not have to change,
        // just the messages
        if (planetsTrack.Count == 0)
            ResetPlanetTrack();
        if (planetsTrack.Count == 0)
            yield break;
        int rand = Random.Range(0, planetsTrack.Count);
        
        // set idle message text
        message = planetsTrack [rand].GetComponent<planetMessaging>().GetIdleMessage();
        if (!message)
            StartCoroutine(UpdateIdleMessages());
        
        // play animation
        phone.AddNewIdleMessage(message);
        
        // remove planet from track list to not repeat it immidiately again
        planetsTrack.RemoveAt(rand);

        StartCoroutine(UpdateIdleMessages());
    }

    public void SendNewMessage(GameObject planet)
    {
        // set idle message text
        message = planet.GetComponent<planetMessaging>().GetDamageMessage();
        if (!message)
            return;

        // play animation
        phone.AddNewMessage(message);
    }
}
