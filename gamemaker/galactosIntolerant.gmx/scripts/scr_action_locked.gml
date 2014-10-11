for (i=0; i<global.planet_count; i+=1)
{
    planet = global.planets[i]
    if (planet.action_lock)
        return true
}
return false
