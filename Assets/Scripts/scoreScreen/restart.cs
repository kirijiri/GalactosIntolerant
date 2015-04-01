using UnityEngine;
using System.Collections;

// This will start the first level
public class restart : MonoBehaviour
{
    public void loadLevel()
    {
		sceneManager.GoToTitleScreen();
    }
}
