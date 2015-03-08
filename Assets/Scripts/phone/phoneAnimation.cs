using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneAnimation : MonoBehaviour
{
    private Rect defaultBox;
    private float top = 140.0f;
    private float[] margins;
    private float characterWidth;
    private float messageVSpace;
    private float maxHeight;

    private List<messageBox> messageBoxes = new List<messageBox>();
    private List<messageBox> messageToRemove = new List<messageBox>();
    private messageBox newMessage;

    private tinker tinker;

    GUIStyle textFont;

    void Start()
    {
        tinker = GameObject.Find("tinker").GetComponent<tinker>();

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
    }

    public void AddNewMessage(string message)
    {
        print("Add New Message "+ message);

        // create new message box
        newMessage = new messageBox();
        newMessage.text = message;
        float height = Mathf.Ceil((message.Length*characterWidth)/defaultBox.width) * defaultBox.height + margins[1] + margins[3];
        newMessage.size = new Rect(defaultBox.x, defaultBox.y, defaultBox.width, height);
        newMessage.CreateTexture();

        // move all the other boxes down
        // remove the ones that go out of bounds
        messageToRemove.Clear();
        foreach(messageBox box in messageBoxes)
        {
            float space = (newMessage.size.height + messageVSpace);
            box.size.y += space;

            if ((box.size.y + box.size.height) > maxHeight)
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
            GUI.DrawTexture(box.size, box.tx); //Draws the texture for the entire screen (width, height)
            Rect textRect = box.size;
            textRect.x = box.size.x + margins[0];
            textRect.y = box.size.y + margins[1];
            textRect.width = textRect.width - (margins[0] + margins[2]);
            textRect.height = textRect.height - (margins[1] + margins[3]);
            GUI.Label (textRect, box.text, textFont);
        }
    }
}

public class messageBox
{
    public Rect size;
    public string text;
    public Texture2D tx;

    public void CreateTexture()
    {
        tx = new Texture2D(1, 1);           //Creates 2D texture
        tx.SetPixel(1, 1, Color.white);     //Sets the 1 pixel to be white
        tx.Apply();                         //Applies all the changes made
    }
}
