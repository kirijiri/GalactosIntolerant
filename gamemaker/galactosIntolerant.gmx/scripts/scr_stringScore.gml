// takes a number and creates a string from it, including commas
my_score = argument0;

my_score = string(my_score)
out_score = ""

for (i=0; i<string_length(my_score); i+=1)
{
    // every third letter, add a comma
    if (not i mod 3) && (i>0)
    {
    out_score += ","
    }
    out_score += my_score
}
