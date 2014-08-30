// arguments
planet = argument0;
new_step = argument1;

// calculations
planet.current_degrees = global.orbit_deg_arr[planet.orbit] * planet.step;
planet.end_degrees = global.orbit_deg_arr[planet.orbit] * new_step;
planet.orbit_deg  = end_degrees - current_degrees;
planet.orbit_incr = planet.orbit_deg / global.anim_time;
