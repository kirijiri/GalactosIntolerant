using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class queue : MonoBehaviour
{
    private List<GameObject> _queue = new List<GameObject>();
    private float _spacing = 0.05f;

    // 
    void Update()
    {
        GameObject previous = null;
        foreach (GameObject go in _queue)
        {
            if (!previous)
                go.GetComponent<message>().Move();
            else
            {
                bool tgt_reached = previous.GetComponent<message>().TargetReached();
                if (tgt_reached)
                    go.GetComponent<message>().Move();

                bool oob = previous.GetComponent<message>().OutOfBounds();
                if (oob)
                {
                    _queue.Remove(previous);
                    Destroy(previous);
                }
            }
            previous = go;
        }
    }

    public void AddMessage(GameObject go)
    {
        _queue.Add(go);

        float height = go.GetComponent<message>().getHeight() + _spacing;
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Tweet");
        foreach (GameObject n in notes)
        {
            n.SendMessage("MoveBy", height);
        }
    }
}
