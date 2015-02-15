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

    // get planet settings from settings class (easier to set up)
    private planetSettings planetSettings;

    //-------------------------------------------------------------------

    void Start()
    {
        planetSettings = GetComponent<planetSettings>();
        idleMessages = planetSettings.idleMessages;
        minorMessages = planetSettings.minorMessages;
        majorMessages = planetSettings.majorMessages;

        ResetIdleMessage();
        ResetMinorMessage();
        ResetMajorMessage();
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
        if (idleTrack.Count == 0)
            ResetIdleMessage();
        int rand = Random.Range(0, idleTrack.Count);
        
        string idle = idleMessages [idleTrack [rand]];
        idleTrack.RemoveAt(rand);
        return idle;
    }
}
