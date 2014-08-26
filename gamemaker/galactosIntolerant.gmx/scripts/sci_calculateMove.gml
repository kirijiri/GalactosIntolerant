// arguments
planet = argument0;
new_step = argument1;

// calculations
planet.current_degrees = global.orbit_deg_arr[planet.orbit] * planet.step;
planet.end_degrees = global.orbit_deg_arr[planet.orbit] * new_step;
planet.move_deg  = end_degrees - current_degrees;
planet.move_incr = move_deg / global.anim_time;
