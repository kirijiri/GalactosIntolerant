// takes a number and creates a string from it, including commas
my_score = argument0;
sep = ","

//convert to string and make an empty string to build
my_score = string(my_score)
out_score = ""
str_len = string_length(my_score)

for (i=0; i<str_len; i+=1)
{
    if (((str_len-(i)) mod 3 == 0 ) && i>0)
        out_score += sep
    out_score += string_char_at(my_score, i+1)
}
return out_score;
