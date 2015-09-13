using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneSetup : MonoBehaviour
{
    private GameObject _tweets_go;
    public Dictionary<string, Dictionary<string, List<Texture2D>>> _message_dict = new Dictionary<string, Dictionary<string, List<Texture2D>>>();
    private queue _queue;

    void Start()
    {
        _tweets_go = GameObject.Find("tweets");
        _queue = GetComponent<queue>();
    }

    // fill dictionary with message textures
    void Awake()
    {
        Texture2D[] tx2d_array = Resources.LoadAll<Texture2D>("Tweets");
        foreach (Texture2D tx2d in tx2d_array)
        {
            string[] splitArray = tx2d.ToString().Split('_');

            if (!_message_dict.ContainsKey(splitArray [0]))
                _message_dict [splitArray [0]] = new Dictionary<string, List<Texture2D>>();

            if (!_message_dict [splitArray [0]].ContainsKey(splitArray [1]))
                _message_dict [splitArray [0]] [splitArray [1]] = new List<Texture2D>();

            _message_dict [splitArray [0]] [splitArray [1]].Add(tx2d);
        }
    }

    public void AddNewMessage(Texture2D tx2d)
    {
        StartCoroutine(CreateMessage(tx2d));
    }
    
    IEnumerator CreateMessage(Texture2D tx2d)
    {
        if (!tx2d)
            yield break;
        
        GameObject go = Instantiate(Resources.Load("messages")) as GameObject; 
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

        //Texture2D tx2d = Resources.Load("Tweets/magmatoel_major_000") as Texture2D;
        Sprite new_spr = Sprite.Create(tx2d, new Rect(0, 0, tx2d.width, tx2d.height), new Vector2(0.0f, 1.0f));
        sr.sprite = new_spr;

        // set attributes
        go.AddComponent<message>();
        go.tag = "Tweet";
        sr.sortingLayerName = "phone";
        sr.sortingOrder = 1;
        go.transform.parent = _tweets_go.transform;

        // add to queue for movement
        _queue.AddMessage(go);
    }
}   


