/*
This script will return 
0 - if the array is NOT all zero
1 - if array is all zero
*/
array = argument0;

array_empty = 1;
for (i=0; i<array_length_1d(array); i++)
    if (global.planets_action_lock[i] > 0 )
        array_empty = 0;
        
return array_empty;
