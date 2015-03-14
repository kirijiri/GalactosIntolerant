using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneAnimation : MonoBehaviour
{
    private Rect defaultBox;
    private float phoneX;
    private float phoneY;
    private float phoneWidth;
    private float phoneHeight;
    private float messageHeight;

    private float[] margins;
    private float characterWidth;
    private float messageVSpace;
    private float scrollSpeed;
    private float screenResolutionFix;
    private Vector3 phonePos;

    Rect sprRect;
    int fontSize;

    private List<messageBox> messageBoxes = new List<messageBox>();
    private List<messageBox> messageToRemove = new List<messageBox>();
    private messageBox newMessage;

    private tinker tinker;
    private int messageID = 0;

    GUIStyle textFont;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        
        // define message font
        textFont = new GUIStyle();
        fontSize = 18;
        textFont.normal.textColor = Color.black;
        textFont.wordWrap = true;
        textFont.font = (Font)Resources.Load("Fonts/MunroSmall");

        // find phone to convert GUI to world space
        GameObject phone = GameObject.Find("phone_bg_256");
        SpriteRenderer sprRen = phone.GetComponent<SpriteRenderer>();
        sprRect = sprRen.sprite.rect;
    }

    void FixedUpdate() 
    {
        textFont.fontSize = Mathf.Min(Mathf.FloorToInt(Screen.width * fontSize/1000), Mathf.FloorToInt(Screen.height * fontSize/1000));
        screenResolutionFix = Screen.width / 19.2f / 100;
    }

    void Update()
    {
        messageVSpace = 20.0f;
        margins = new float[4]{10.0f, 10.0f, 10.0f, 10.0f}; //left, top, right, bottom
        for (int i = 0; i < margins.Length; i++) margins[i] *= screenResolutionFix;
        characterWidth = 10.0f;
        scrollSpeed = 2.0f;
        
        phoneX = 52.0f;
        phoneY = 250.0f;
        phoneWidth = 400.0f;
        phoneHeight = 380.0f;
        messageHeight = 30.0f;

        defaultBox = new Rect(phoneX, phoneY, phoneWidth, messageHeight);

        /*
        phoneX = tinker.phoneX;
        phoneY = tinker.phoneY;

        messageHeight = tinker.messageHeight;

        defaultBox = new Rect(phoneX, phoneY, tinker.messageWidth, tinker.messageHeight);
        margins = tinker.messageMargins;
        characterWidth = tinker.characterWidth;
        messageVSpace = tinker.messageVSpace;
        scrollSpeed = tinker.scrollSpeed;
        */
    }

    public void AddNewMessage(string message)
    {
        //print("Add New Message "+ message);

        // create new message box
        newMessage = new messageBox();
        newMessage.CreateTexture();
        messageID += 1;
        newMessage.id = messageID;

        newMessage.text = message;

        newMessage.size = defaultBox;
        newMessage.size.height = Mathf.Ceil((message.Length*characterWidth)/defaultBox.width) * defaultBox.height + margins[1] + margins[3];
        newMessage.newY = newMessage.size.y; 

        // move all the other boxes down
        // remove the ones that go out of bounds
        messageToRemove.Clear();
        foreach(messageBox box in messageBoxes)
        {
            box.time = Time.time;
            box.oldY = box.size.y;
            box.newY += newMessage.size.height + messageVSpace;
            if ((box.newY + box.size.height) > phoneHeight)
            {
                messageToRemove.Add(box);
            }
        }
        foreach(messageBox box in messageToRemove)
        {
            messageBoxes.Remove(box);
        }

        messageBoxes.Insert(0, newMessage);
    }

    void OnGUI()
    {
        foreach(messageBox box in messageBoxes)
        {
            if (box.size.y < box.newY)
            {
                print ("move "+box.id+" ->" +box.size.y +" to "+box.newY);
                float lerp = Mathf.Lerp (box.oldY, box.newY, scrollSpeed * (Time.time - box.time));
                box.size.y = lerp;
            }

            // convert position into world space (avoid floating)
            Rect curPos = box.size;
            //curPos.x += phonePos[0];
            //curPos.y += phonePos[1];

            curPos.x *= screenResolutionFix;
            curPos.y *= screenResolutionFix;
            curPos.width *= screenResolutionFix;
            curPos.height *= screenResolutionFix;

            // draw box
            GUI.DrawTexture(curPos, box.tx); 

            // add margin for the text
            Rect textRect = curPos;
            textRect.x = curPos.x + margins[0];
            textRect.y = curPos.y + margins[1];
            textRect.width = textRect.width - (margins[0] + margins[2]);
            textRect.height = textRect.height - (margins[1] + margins[3]);

            // draw text
            GUI.Label (textRect, box.text, textFont);
        }
    }
}

public class messageBox
{
    public Rect size;
    public float newY;
    public float oldY;
    public string text;
    public Texture2D tx;
    public float time;
    public int id;

    public void CreateTexture()
    {
        tx = new Texture2D(1, 1);           //Creates 2D texture
        tx.SetPixel(1, 1, Color.white);     //Sets the 1 pixel to be white
        tx.Apply();                         //Applies all the changes made
    }
}
