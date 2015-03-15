using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class handles the planets messaging system.

public class planetMessaging : MonoBehaviour
{
    private string[] idleMessages;
    private string[] minorMessages;
    private string[] majorMessages;
    
    // lists to keep track of data that has been used before
    private List<int> idleTrack = new List<int>();
    private List<int> minorTrack = new List<int>();
    private List<int> majorTrack = new List<int>();

    private float criticalPopulationPercentage;

    // get planet settings from settings class (easier to set up)
    private planetSettings planetSettings;
    private tinker tinker;

    //-------------------------------------------------------------------

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        planetSettings = GetComponent<planetSettings>();
        idleMessages = planetSettings.idleMessages;
        minorMessages = planetSettings.minorMessages;
        majorMessages = planetSettings.majorMessages;

        ResetIdleMessage();
        ResetMinorMessage();
        ResetMajorMessage();
    }

    void Update ()
    {
        criticalPopulationPercentage = tinker.criticalPopulationPercentage;
    }

    //------------------------------------------------------------------- reset

    void ResetIdleMessage()
    {
        for (int i = 0; i < idleMessages.Length; i++)
            idleTrack.Add(i);
    }
    
    void ResetMinorMessage()
    {
        for (int i = 0; i < minorMessages.Length; i++)
            minorTrack.Add(i);
    }
    
    void ResetMajorMessage()
    {
        for (int i = 0; i < majorMessages.Length; i++)
            majorTrack.Add(i);
    }

    //------------------------------------------------------------------- get
    
    // returns a random idle message that has not been used 
    // resets the unused list if it runs out of messages
    public string GetIdleMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0) return "";

        // reset counter if every message has been used already
        if (idleTrack.Count == 0)
            ResetIdleMessage();
        int rand = Random.Range(0, idleTrack.Count);
        
        string message = idleMessages [idleTrack [rand]];
        idleTrack.RemoveAt(rand);
        return message;
    }

    
    public string GetMinorMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0) return "";

        // reset counter if every message has been used already
        if (minorTrack.Count == 0)
            ResetMinorMessage();
        int rand = Random.Range(0, minorTrack.Count);
        
        string message = minorMessages [minorTrack [rand]];
        minorTrack.RemoveAt(rand);
        return message;
    }

    
    public string GetMajorMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0) return "";

        // reset counter if every message has been used already
        if (majorTrack.Count == 0)
            ResetMajorMessage();
        int rand = Random.Range(0, majorTrack.Count);
        
        string message = majorMessages [majorTrack [rand]];
        majorTrack.RemoveAt(rand);
        return message;
    }

    public string GetDamageMessage()
    {
        if (planetSettings.population >= (planetSettings.maxPopulation/100*criticalPopulationPercentage))
            return GetMinorMessage();
        else
            return GetMajorMessage();
    }
}
