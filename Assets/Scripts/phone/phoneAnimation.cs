using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneAnimation : MonoBehaviour
{
    private Rect defaultBox;
    private float[] margins;
    private float characterWidth;
    private float messageVSpace;
    private float maxHeight;
    private float scrollSpeed;

    private List<messageBox> messageBoxes = new List<messageBox>();
    private List<messageBox> messageToRemove = new List<messageBox>();
    private messageBox newMessage;

    private tinker tinker;

    GUIStyle textFont;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();
        
        // define message font
        textFont = new GUIStyle();
        textFont.fontSize = 17;
        textFont.normal.textColor = Color.black;
        textFont.wordWrap = true;
        textFont.font = (Font)Resources.Load("Fonts/MunroSmall");
    }

    void Update()
    {
        defaultBox = new Rect(tinker.messageX, tinker.messageY, tinker.messageWidth, tinker.messageHeight);
        margins = tinker.messageMargins;
        characterWidth = tinker.characterWidth;
        messageVSpace = tinker.messageVSpace;
        maxHeight = tinker.phoneHeight;
        scrollSpeed = tinker.scrollSpeed;
    }

    public void AddNewMessage(string message)
    {
        //print("Add New Message "+ message);

        // create new message box
        newMessage = new messageBox();
        newMessage.text = message;
        float height = Mathf.Ceil((message.Length*characterWidth)/defaultBox.width) * defaultBox.height + margins[1] + margins[3];
        newMessage.size = new Rect(defaultBox.x, defaultBox.y, defaultBox.width, height);
        newMessage.newY = newMessage.size.y;
        newMessage.CreateTexture();

        // move all the other boxes down
        // remove the ones that go out of bounds
        messageToRemove.Clear();
        foreach(messageBox box in messageBoxes)
        {
            box.time = Time.time;
            box.oldY = box.size.y;
            box.newY += newMessage.size.height + messageVSpace;
            if ((box.newY + box.size.height) > maxHeight)
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
                float lerp = Mathf.Lerp (box.oldY, box.newY, scrollSpeed * (Time.time - box.time));
                box.size.y = lerp;
            }

            // convert position into world space (avoid floating)
            Rect curPos = box.ConvertSpace();

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

    private Vector3 phonePos;

    public Rect ConvertSpace() 
    {
        // find phone to convert GUI to world space
        GameObject phone = GameObject.Find("phone_bg_256");
        SpriteRenderer sprRen = phone.GetComponent<SpriteRenderer>();
        Rect sprRect = sprRen.sprite.rect;
        phonePos = Camera.main.WorldToScreenPoint(phone.transform.position);

        // TODO: get extra sprite just for message window, so I get 
        // rid of the hardcoded bits
        Rect newPos = size;
        newPos.x += (float)(phonePos[0] - (sprRect.width-20)/2);
        newPos.y += (float)(phonePos[1] - (sprRect.height+100)/2);

        return newPos;
    }

    public void CreateTexture()
    {
        tx = new Texture2D(1, 1);           //Creates 2D texture
        tx.SetPixel(1, 1, Color.white);     //Sets the 1 pixel to be white
        tx.Apply();                         //Applies all the changes made
    }
}
