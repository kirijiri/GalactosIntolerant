using UnityEngine;
using System.Collections;

public class followerCount : MonoBehaviour
{
    private float bgWidth;
    private float bgHeight;
    private float screenHeight;
    private int fontSize;
    private Rect textRect;
    private float x;
    private float y;
    private float w;
    private float h;
    private GUIStyle textStyle;
    private tinker tinker;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        // define message font
        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.black;
        textStyle.wordWrap = true;
        textStyle.font = (Font)Resources.Load("Fonts/MunroSmall");
    }
    
    void FixedUpdate()
    {
        textStyle.fontSize = Mathf.Min(Mathf.FloorToInt(Screen.width * fontSize / 1000), Mathf.FloorToInt(screenHeight * fontSize / 1000));
    }
    
    void Update()
    {
        screenHeight = Screen.width / 16 * 9;
        bgWidth = tinker.bgWidth;
        bgHeight = tinker.bgHeight;

        fontSize = tinker.followerFontSize;
        x = tinker.followerX;
        y = tinker.followerY;
        w = tinker.followerWidth;
        h = tinker.followerHeight;

        x = x / bgWidth * Screen.width;
        y = y / bgHeight * screenHeight;
        w = w / bgWidth * Screen.width;
        h = h / bgHeight * screenHeight;
        textRect = new Rect(x, y, w, h);
    }

    void OnGUI()
    {
        textStyle.font = (Font)Resources.Load("Fonts/MunroSmall");
        GUI.Label(textRect, "Followers: " + gameManager.Instance.followers.ToString("F0"), textStyle);
    }

}
