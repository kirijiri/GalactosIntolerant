using UnityEngine;
using System.Collections;

public class planetImageSpriteControl : MonoBehaviour
{
    public GameObject parent;

    void Update()
    {
        transform.position = parent.transform.position;
    }
}
