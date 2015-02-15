using UnityEngine;
using System.Collections;

// This class controls the image prefab of the planets
// It will keep the sprite from rotating with the rigidbody 

public class planetImageSpriteControl : MonoBehaviour
{
    public GameObject parent;

    void Update()
    {
        transform.position = parent.transform.position;
    }
}
