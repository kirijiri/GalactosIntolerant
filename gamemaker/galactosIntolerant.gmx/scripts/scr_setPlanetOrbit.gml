planet = argument0;
abs_degrees  = argument1;

radius = global.sun_radius + (planet.orbit * global.orb_width);

planet.x = (room_width/2) + global.offset_x + (radius * (sin(degtorad(global.offset_rotation + abs_degrees))));
planet.y = (room_height/2)+ global.offset_y + (radius * (cos(degtorad(global.offset_rotation + abs_degrees))));
