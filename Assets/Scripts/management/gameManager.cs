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

public class gameManager : Object
{
    protected gameManager() {}

    private static gameManager instance = null;

    public event OnStateChangeHandler OnStateChange;

    public  GameState gameState { get; private set; }
    
    public static gameManager Instance
    {
        get
        {
            if (gameManager.instance == null)
            {
                DontDestroyOnLoad(gameManager.instance);
                gameManager.instance = new gameManager();
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
