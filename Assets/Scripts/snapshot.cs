using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

// This class is dealing with the snapshot button behaviour.

public class snapshot : MonoBehaviour
{
    private gravityBeam gravityBeam;
    private bool doSnapshot = false;
    private float time;
    private float fadeSpeed;
    private tinker tinker;

    //-------------------------------------------------------------------

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        gravityBeam = GameObject.Find("gravityBeam").GetComponent<gravityBeam>();
        
        // make button invisible (gravity beam will make it visible)
        renderer.enabled = false;
    }

    void OnMouseDown()
    {
        if (!renderer.enabled)
            return;

        if (!gravityBeam.isActive && gravityBeam.available) 
            return;
        
        // save stats
        gameManager.Instance.alignedPlanetCount = gravityBeam.GetAlignedPlanetCount();

        // animate flash
        doSnapshot = true;
        StartCoroutine(WaitAndScoreScreen());
    }

    IEnumerator WaitAndScoreScreen()
    {
        time = Time.time;
        yield return new WaitForSeconds(0.25f);

        // switch screens
        sceneManager.GoToScoreScreen();
        yield break;
    }

    void Update()
    {
        fadeSpeed = tinker.flashFadeInSpeed;
    }

    void OnGUI()
    {
        if (!doSnapshot)
            return;

        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

        Texture2D tx;
        tx = new Texture2D(1, 1);          
        Color lerpedColor = Color.Lerp(Color.clear, Color.white, (Time.time - time) * fadeSpeed);
        tx.SetPixel(1, 1, lerpedColor);    
        tx.Apply();                        

        GUI.DrawTexture(screenRect, tx); 
    }
}
