using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class handles the planets messaging system.

public class planetMessaging : MonoBehaviour
{
    private List<Texture2D> idleMessages;
    private List<Texture2D> minorMessages;
    private List<Texture2D> majorMessages;
    
    // lists to keep track of data that has been used before
    private List<int> idleTrack = new List<int>();
    private List<int> minorTrack = new List<int>();
    private List<int> majorTrack = new List<int>();
    private float criticalPopulationPercentage;

    // get planet settings from settings class (easier to set up)
    private planetSettings planetSettings;
    private phoneSetup phoneSetup;
    private tinker tinker;

    //-------------------------------------------------------------------

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        planetSettings = GetComponent<planetSettings>();
        phoneSetup = GameObject.Find("phone_setup").GetComponent<phoneSetup>();
        idleMessages = phoneSetup._message_dict [planetSettings.race.ToLower()] ["idle"];
        minorMessages = phoneSetup._message_dict [planetSettings.race.ToLower()] ["minor"];
        majorMessages = phoneSetup._message_dict [planetSettings.race.ToLower()] ["major"];

        ResetIdleMessage();
        ResetMinorMessage();
        ResetMajorMessage();
    }

    void Update()
    {
        criticalPopulationPercentage = tinker.criticalPopulationPercentage;
    }

    //------------------------------------------------------------------- reset

    void ResetIdleMessage()
    {
        for (int i = 0; i < idleMessages.Count; i++)
            idleTrack.Add(i);
    }
    
    void ResetMinorMessage()
    {
        for (int i = 0; i < minorMessages.Count; i++)
            minorTrack.Add(i);
    }
    
    void ResetMajorMessage()
    {
        for (int i = 0; i < majorMessages.Count; i++)
            majorTrack.Add(i);
    }

    //------------------------------------------------------------------- get
    
    // returns a random idle message that has not been used 
    // resets the unused list if it runs out of messages
    public Texture2D GetIdleMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0)
            return null;

        // reset counter if every message has been used already
        if (idleTrack.Count == 0)
            ResetIdleMessage();
        int rand = Random.Range(0, idleTrack.Count - 1);
        
        Texture2D message = idleMessages [idleTrack [rand]];
        idleTrack.RemoveAt(rand);
        return message;
    }

    public Texture2D GetMinorMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0)
            return null;

        // reset counter if every message has been used already
        if (minorTrack.Count == 0)
            ResetMinorMessage();
        int rand = Random.Range(0, minorTrack.Count - 1);

        Texture2D message = minorMessages [minorTrack [rand]];
        minorTrack.RemoveAt(rand);
        return message;
    }
    
    public Texture2D GetMajorMessage()
    {
        // drop out of planet is dead
        if (planetSettings.population == 0)
            return null;

        // reset counter if every message has been used already
        if (majorTrack.Count == 0)
            ResetMajorMessage();
        int rand = Random.Range(0, majorTrack.Count - 1);

        Texture2D message = majorMessages [majorTrack [rand]];
        majorTrack.RemoveAt(rand);
        return message;
    }

    public Texture2D GetDamageMessage()
    {
        if (planetSettings.population >= (planetSettings.maxPopulation / 100 * criticalPopulationPercentage))
            return GetMinorMessage();
        else
            return GetMajorMessage();
    }
}
