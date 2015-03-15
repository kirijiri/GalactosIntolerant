using UnityEngine;
using System.Collections;

// Game States
public enum GameState
{
    TITLE,
    LEVEL1,
    SCORE
}

public delegate void OnStateChangeHandler();

public class gameManager 
{
    public int alignedPlanetCount = 0;
    public int followers = 0;

    private gameManager() {}

    private static gameManager instance = null;

    public event OnStateChangeHandler OnStateChange;

    public  GameState gameState { get; private set; }
    
    public static gameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new gameManager();
            }
            return gameManager.instance;
        }
    }
    
    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }
    
    public void OnApplicationQuit()
    {
        gameManager.instance = null;
    }
}
