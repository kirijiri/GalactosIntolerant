using UnityEngine;
using System.Collections;

// This class is for switching between screens/levels
// It is static, so the functions can be accessed from anywhere

public static class sceneManager
{
    public static void GoToScoreScreen()
    {
        Application.LoadLevel("scoreScreen");
    }

    public static void GoToLevel1Screen()
    {
        Application.LoadLevel("scene1");
    }
}
