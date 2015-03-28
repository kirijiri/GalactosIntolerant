using UnityEngine;
using System.Collections;

// This class stores the planets individual attributes.
// Used for easier access and modification of the planet

public class planetSettings : MonoBehaviour
{
    // planet stats
    public float speed = 0;
    public int size = 14;
    public float orbitRadius = 0;
    public double population = 0;
    public double maxPopulation;
    public double followers = 0;
    public string planetName = "";

    // scoring
    public float flickUpDeathPercentage = 0.3f;
    public float bleedPercentage = 0.005f;
    public float holdDownBleedMultilier = 10f;

    // flavour
    public string biome = "";
    public string race = "";
    public string look = "";
    public string note = "";
    public string[] idleMessages;
    public string[] minorMessages;
    public string[] majorMessages;
}
