using UnityEngine;
using System.Collections;

public class scoring : MonoBehaviour
{
    private GameObject[] planets;
    private GameObject planet;
    private planetInit init;
    private planetSettings settings;
	private planetsIndividualSound planetSounds;
    
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

            // choose the bigger evil of hold/gravity
            float bleed = 1.0f;
            if (init.gravityBleedMultilier > bleed) bleed = init.gravityBleedMultilier;
            else if (init.bleedMultilier > bleed) bleed = init.bleedMultilier;

            if (init.do_kill_people && settings.population > 0.0f)
            {
                diffMagnitude = Mathf.Abs(init.initVelocity.magnitude - planet.GetComponent<Rigidbody2D>().velocity.magnitude);
                settings.population -= diffMagnitude * (settings.maxPopulation * settings.bleedPercentage * bleed) * Time.deltaTime;

                // planet dead
                if (settings.population < 0.0f)
                {  
                    settings.population = 0.0f;
                }
            }

			PlayDamageSounds(settings, planet);
            UpdateDeceaseCount();   
        }
    }

	private void PlayDamageSounds(planetSettings settings, GameObject planet)
	{
		planetSounds = planet.GetComponent<planetsIndividualSound>();
		if ((settings.population / settings.maxPopulation) < 0.333)
		{
			planetSounds.AudioDamaged();
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

    public void IncreaseDeathsGravity(GameObject planet)
    {
        planet.GetComponent<planetInit>().gravityBleedMultilier = planet.GetComponent<planetSettings>().gravityBeamBleedMultiplier;
        planet.GetComponent<planetInit>().do_kill_people = true;
    }

    public void DecreaseDeathsGravity(GameObject planet)
    {
        planet.GetComponent<planetInit>().gravityBleedMultilier = 1.0f;
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
