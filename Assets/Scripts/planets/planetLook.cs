using UnityEngine;
using System.Collections;

public static class planetLook
{
    public static void gravity(this GameObject go)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    
    public static void aligned(this GameObject go)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

    public static void standard(this GameObject go)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }
}
