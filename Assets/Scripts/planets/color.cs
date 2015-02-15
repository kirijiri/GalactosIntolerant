using UnityEngine;
using System.Collections;

public static class color
{
	public static void red (this GameObject go) 
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
	}
    
    public static void blue (this GameObject go) 
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

    public static void white (this GameObject go) 
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }
}
