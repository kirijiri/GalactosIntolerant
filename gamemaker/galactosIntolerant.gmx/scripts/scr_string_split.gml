/*
 argument0 = string you want to split up 
 argument1 = substring you want to split the string by
 
 fx.
 argument0 = "hello|very|very|cruel|world"
 arguemnt1 = "|"
 
 array[0] = "hello"
 array[1] = "very"
 ...
 array[4] = "world"
*/

my_string = argument0;
delimiter = argument1;

// occurance of delimiter in input string
numbers = string_count(delimiter, my_string);

// split string and append to array
for( i=0; i<numbers; i+=1 )
{
    pos = string_pos(delimiter, my_string);
    array[i] = string(string_copy(my_string, 1, pos-1));
    my_string = string_delete(my_string, 1, pos);
    //my_string = string_replace_all(my_string, '\n', '');
}
// append last bit of string to array
array[i] = my_string;

return array
