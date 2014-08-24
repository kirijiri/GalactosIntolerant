list = argument0;
count = argument1;

var i;
var maxm = -1;
var res = -1;

//arr[0,1,2,3,4,5,6,7]
for (i = 0; i < global.cardinal_count; i += 1)
{
    arr[i] = 0;
}

for (i = 0; i < count; i += 1)
{
    arr[list[i]] += 1;
}
for (i = 0; i < global.cardinal_count; i += 1)
{
    if (arr[i] > maxm) 
    {
        maxm = arr[i];
        res = i;
    }
}
return res;
