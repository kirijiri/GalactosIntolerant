/*

// parse planet data textfile
show_debug_message("------------------parse filename-------------------");

file = file_text_open_read(global.file_message_data);
c = 0;
while (!file_text_eof(file))
{
   data_array[c] = file_text_readln(file);
   c++;
}
file_text_close(file);

a = scr_string_split(data_array[i], "=");
for( var j=0; j<array_length_1d(a); j+=1 )
{
    show_debug_message(a[j]);
}
*/

/*
/// TODO: planet data

// parse planet data textfile
show_debug_message("------------------parse filename-------------------");

file = file_text_open_read(global.file_planet_data);
c = 0;
while (!file_text_eof(file))
{
   data_array[c] = file_text_readln(file);
   c++;
}
file_text_close(file);

var planet_count = -1;
for (i=0; i<c; i++)
{
    a = scr_string_split(data_array[i], "=");
    //for( var j=0; j<array_length_1d(a); j+=1 )
    //{
    //    show_debug_message(a[j]);
    //}
    
    show_debug_message(array_length_1d(a));
    show_debug_message(a[0]);
    show_debug_message(string(a[0])+" ==? startplanet ??? "+string(a[0] == "startplanet"));
    show_debug_message(is_string(a[0]));
    show_debug_message(string_length(a[0]));
    show_debug_message(string_length("startplanet"));
    
    if (a[0] == "startplanet") 
    {
        show_debug_message("START");
        planet_count++;
    }   
    if (array_length_1d(a) == 2)
    {
        show_debug_message("planet_count:" + string(planet_count));
        if      (a[0] == "name") planet_names[planet_count] = a[1];
        else if (a[0] == "size") planet_sizes[planet_count] = a[1];
        else if (a[0] == "race") planet_races[planet_count] = a[1];
        else if (a[0] == "population") planet_populations[planet_count] = a[1];
        else if (a[0] == "follow") planet_follows[planet_count] = a[1];
        else if (a[0] == "damspeedup") planet_damspeedups[planet_count] = a[1];
        else if (a[0] == "damspeeddown") planet_damspeeddowns[planet_count] = a[1];
        else if (a[0] == "damorbit1") planet_damorbit1s[planet_count] = a[1];
        else if (a[0] == "damorbit2") planet_damorbit2s[planet_count] = a[1];
        else if (a[0] == "damorbit3") planet_damorbit3s[planet_count] = a[1];
        else if (a[0] == "damorbit4") planet_damorbit4s[planet_count] = a[1];
        else if (a[0] == "damorbit5") planet_damorbit5s[planet_count] = a[1];
        else if (a[0] == "damorbit6") planet_damorbit6s[planet_count] = a[1];
        else if (a[0] == "wikitext") planet_wikitexts[planet_count] = a[1];
    }
}
*/
