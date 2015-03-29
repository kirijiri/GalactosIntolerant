using UnityEngine;
using System.Collections;

public class scoring : MonoBehaviour
{
    private GameObject[] planets;
    private GameObject planet;
    private planetInit init;
    private planetSettings settings;
    
    private float diffMagnitude;

    // Use this for initialization
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");

        double followers = 0;
        for (int i = 0; i < planets.Length; i++)
        {
            followers += (planets [i].GetComponent<planetSettings>().population * planets [i].GetComponent<planetSettings>().followers);
        }
        gameManager.Instance.followers = followers;
    }
    
    // Update is called once per frame
    void Update()
    {
        // bleed all planets
        for (int i = 0; i < planets.Length; i++)
        {
            planet = planets[i];
            settings = planet.GetComponent<planetSettings>();
            init = planet.GetComponent<planetInit>();
            if (init.do_kill_people && settings.population > 0.0f)
            {
                diffMagnitude = Mathf.Abs(init.initVelocity.magnitude - planet.rigidbody2D.velocity.magnitude);
                settings.population -= diffMagnitude * (settings.maxPopulation * settings.bleedPercentage * init.bleedMultilier);

                // planet dead
                if (settings.population < 0.0f)
                {  
                    settings.population = 0.0f;
                }
            }
            UpdateDeceaseCount();   
        }
    }

    public void IncreaseDeaths(GameObject planet)
    {
        planet.GetComponent<planetInit>().bleedMultilier = planet.GetComponent<planetSettings>().holdDownBleedMultilier;
        planet.GetComponent<planetInit>().do_kill_people = true;
    }
    
    public void DecreaseDeaths(GameObject planet)
    {
        planet.GetComponent<planetInit>().bleedMultilier = 1;
    }

    public void KillPeople(GameObject planet)
    {
        settings = planet.GetComponent<planetSettings>();
        settings.population -= settings.maxPopulation * settings.flickUpDeathPercentage;
        
        if (settings.population < 0.0f)
            settings.population = 0.0f;
    }

    public void UpdateDeceaseCount()
    {
        double followers = 0;
        double population = 0;
        double dead = 0;
        for (int i = 0; i < planets.Length; i++)
        {
            settings = planets[i].GetComponent<planetSettings>();
            followers += (settings.population * settings.followers);
            population += settings.population;
            dead += (settings.maxPopulation - settings.population);
        }
        gameManager.Instance.followers = followers;
        gameManager.Instance.population = population;
        gameManager.Instance.dead = dead;
    }
}
