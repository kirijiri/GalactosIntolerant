// arguments
orbit = argument0;
step = argument1;
new_step = argument2;

// calculations
current_degrees = global.orbit_deg_arr[self.orbit] * step;
end_degrees = global.orbit_deg_arr[self.orbit] * new_step;

move_deg  = end_degrees - current_degrees;
move_incr = move_deg / global.anim_time;

// returns
return_arr[0] = current_degrees;
return_arr[1] = end_degrees;
return_arr[2] = move_deg;
return_arr[3] = move_incr;
return return_arr;

