/*
nr = argument0;

show_debug_message(nr);

str = string(nr);
show_debug_message(string_length(str));

// split every 3 characters
splitter = 3;


dil = round(string_length(str) / splitter);
show_debug_message(dil);

c = 0;
for( i=1; i<dil; i++)
{
    show_debug_message(string_length(str)-splitter*i-c+1);
    str = string_insert(".", str, string_length(str)-splitter*i-c+1);
    c++;
}

show_debug_message(string(str));
*/

// takes a number and creates a string from it, including commas
my_score = argument0;

//convert to string and make an empty string to build
my_score = string(my_score)
out_score = ""
str_len = string_length(my_score)-1

show_debug_message( "my_score = " + my_score)
for (i=0; i<str_len; i+=1)
{
    show_debug_message( string(i) + "  " + string_char_at(my_score, i))
    if (((str_len-i) mod 3 == 0 ) && i>0)
        out_score += ","
    out_score += string_char_at(my_score, i)
}
show_debug_message( "out_score = " + out_score)

return out_score;
