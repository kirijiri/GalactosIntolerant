using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class queue : MonoBehaviour
{
    private List<GameObject> _queue = new List<GameObject>() {};
    private List<GameObject> _to_delete_queue = new List<GameObject>() {};
    private float _spacing = 0.05f;
    private int _max_number_messages = 3;

    // 
    void Update()
    {
        _to_delete_queue = new List<GameObject>() {};;
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
                    _to_delete_queue.Add(previous);
            }
            previous = go;
        }
        foreach (GameObject go in _to_delete_queue)
        {
            _queue.Remove(go);
            Destroy(go);
            Resources.UnloadUnusedAssets();
        }
    }

    public void AddMessage(GameObject go)
    {
        if (_queue.Count >= _max_number_messages)
        {
            GameObject prev_go = _queue[_queue.Count-1];
            if (!prev_go.GetComponent<message>().IsMoving())
            {
                _queue.Remove(prev_go);
                Destroy(prev_go);
                Resources.UnloadUnusedAssets();
            }
        }
        _queue.Add(go);

        float height = go.GetComponent<message>().getHeight() + _spacing;
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Tweet");
        foreach (GameObject n in notes)
        {
            n.SendMessage("MoveBy", height);
        }
    }
}
