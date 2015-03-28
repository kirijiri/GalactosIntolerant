using UnityEngine;
using System.Collections;

public class keybinding : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.F2))
        {
            sceneManager.GoToLevel1Screen();
        }
    }
}
