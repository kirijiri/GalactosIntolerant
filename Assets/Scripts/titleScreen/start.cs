using UnityEngine;
using System.Collections;

public class start : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            sceneManager.GoToLevel1Screen();
        }
    }
}
