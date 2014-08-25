list = argument0;
count = argument1;

var i = 0;
var card_nr = -1;
var card_planet_count = 0;

// make list of all cardinals initialised with 0
// arr[0,0,0,0,0,0,0,0,etc]
for (i = 0; i < global.cardinal_count; i += 1)
    arr[i] = 0;

// for each occurance of a cardinal nr increase
// cardinal score
for (i = 0; i < count; i += 1)
{
    arr[list[i]] += 1;
}

// determine which cardinal has the highest amount
// of planets on it and what the count is
for (i = 0; i < global.cardinal_count; i += 1)
{
    if (arr[i] > card_planet_count) 
    {
        card_planet_count = arr[i];
        card_nr = i;    
    }
}

// return cardinal number and planet count
r[0] = card_nr;
r[1] = card_planet_count;
return r;
