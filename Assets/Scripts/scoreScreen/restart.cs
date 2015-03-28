using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

// This will start the first level

public class restart : MonoBehaviour
{
    void OnMouseDown()
    {
        sceneManager.GoToLevel1Screen();
    }
}
