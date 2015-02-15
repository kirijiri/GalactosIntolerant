using UnityEngine;
using System.Collections;

public static class shipLook
{

    public static void beamOn(this GameObject go)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }
}
