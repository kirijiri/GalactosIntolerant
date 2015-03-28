using UnityEngine;
using System.Collections;

public static class planetLook
{
    public static void gravity(this GameObject go)
    {
        debug debug = GameObject.Find("debug").GetComponent<debug>();
        if (!debug.showPlanetColors) return;

        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    
    public static void aligned(this GameObject go)
    {
        debug debug = GameObject.Find("debug").GetComponent<debug>();
        if (!debug.showPlanetColors) return;

        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

    public static void standard(this GameObject go)
    {
        debug debug = GameObject.Find("debug").GetComponent<debug>();
        if (!debug.showPlanetColors) return;
        
        SpriteRenderer spriteRenderer = go.GetComponent<planetInit>().planetGraphic.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        if (debug.showPlanetDecayColors)
        {
            float percentage = (float) (100 / go.GetComponent<planetSettings>().maxPopulation * go.GetComponent<planetSettings>().population);
            Color lerpedColor = Color.Lerp(Color.black, Color.white, percentage / 100);
            spriteRenderer.color = lerpedColor;
        }
    }
}
