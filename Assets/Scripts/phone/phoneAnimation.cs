using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneAnimation : MonoBehaviour
{
    private float screenHeight;
    private float bgWidth;
    private float bgHeight;

    private Rect defaultBox;
    private float phoneX;
    private float phoneY;
    private float phoneWidth;
    private float phoneHeight;
    private float messageHeight;

    private float[] margins;
    private float messageVSpace;
    private float scrollSpeed;
    private Vector3 phonePos;

    private GameObject phone;
    private int fontSize;

    private List<messageBox> messageBoxes = new List<messageBox>();
    private List<messageBox> messageToRemove = new List<messageBox>();
    private messageBox newMessage;

    private tinker tinker;
    private int messageID = 0;

    GUIStyle textStyle;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

        bgWidth = 480;//GameObject.Find("backdrop").GetComponent<SpriteRenderer>().sprite.rect.width;
        bgHeight = 270;//bgWidth/16 * 9;
        
        // define message font
        textStyle = new GUIStyle();
        fontSize = 22;
        textStyle.normal.textColor = Color.black;
        textStyle.wordWrap = true;
        textStyle.font = (Font)Resources.Load("Fonts/MunroSmall");

        // find phone to convert GUI to world space
        phone = GameObject.Find("phone_bg_256");
    }

    void FixedUpdate() 
    {
        textStyle.fontSize = Mathf.Min(Mathf.FloorToInt(Screen.width * fontSize/1000), Mathf.FloorToInt(screenHeight * fontSize/1000));
    }

    void Update()
    {
        screenHeight = Screen.width/16 * 9;

        margins = new float[4]{3.0f, 3.0f, 3.0f, 3.0f}; //left, top, right, bottom
        margins[0] = margins[0] / bgWidth * Screen.width;
        margins[1] = margins[1] / bgHeight * screenHeight;
        margins[2] = margins[2] / bgWidth * Screen.width;
        margins[3] = margins[3] / bgHeight * screenHeight;

        messageVSpace = 20.0f;
        scrollSpeed = 2.0f;

        phoneWidth = 98.0f;
        phoneWidth = phoneWidth / bgWidth * Screen.width;
        phoneHeight = 118.0f;
        phoneHeight = phoneHeight / bgHeight * screenHeight;
        print ("phoneHeight: " + phoneHeight);

        phoneX = -phoneWidth/2;
        float phoneYOffset = 10.0f;
        phoneYOffset = phoneYOffset / bgHeight * screenHeight;
        phoneY = -phoneHeight/2 - phoneYOffset;

        defaultBox = new Rect(phoneX, phoneY, phoneWidth, phoneHeight);
        phonePos = Camera.main.WorldToScreenPoint(phone.transform.position);

        /*
        phoneX = tinker.phoneX;
        phoneY = tinker.phoneY;

        messageHeight = tinker.messageHeight;

        defaultBox = new Rect(phoneX, phoneY, tinker.messageWidth, tinker.messageHeight);
        margins = tinker.messageMargins;
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
        GUIContent content = new GUIContent(message);

        newMessage.size = defaultBox;
        Vector2 textSize = textStyle.CalcSize(content);
        newMessage.size.height = Mathf.Ceil(textSize[0] / (defaultBox.width - margins[0] - margins[2])) * textSize[1] + margins[1] + margins[3];
        newMessage.newY = newMessage.size.y; 

        // move all the other boxes down
        // remove the ones that go out of bounds
        messageToRemove.Clear();
        foreach(messageBox box in messageBoxes)
        {
            box.time = Time.time;
            box.oldY = box.size.y;
            box.newY += newMessage.size.height + messageVSpace;

            if ((box.newY + box.size.height) > (phoneHeight+defaultBox.y))
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
                //print ("move "+box.id+" ->" +box.size.y +" to "+box.newY);
                float lerp = Mathf.Lerp (box.oldY, box.newY, scrollSpeed * (Time.time - box.time));
                box.size.y = lerp;
            }

            // convert position into world space (avoid floating)
            Rect curPos = box.size;

            curPos.x += phonePos[0];
            curPos.y += phonePos[1];

            // draw box
            GUI.DrawTexture(curPos, box.tx); 

            // add margin for the text
            Rect textRect = curPos;
            textRect.x = curPos.x + margins[0];
            textRect.y = curPos.y + margins[1];
            textRect.width = curPos.width - margins[2];

            // draw text
            GUI.Label (textRect, box.text, textStyle);
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

