var step = argument0;
var rate = argument1;
var orbit = argument2;

step += rate
degrees = global.orbit_deg_arr[orbit] * step
radius = global.sun_radius + (orbit * global.orb_width)


pos[0] = (room_width/2) + (radius * (sin(degtorad(degrees))))
pos[1] = (room_height/2) + (radius * (cos(degtorad(degrees))))

return pos;
