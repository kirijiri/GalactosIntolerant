using UnityEngine;
using System.Collections;

public class scnScore : MonoBehaviour
{
    gameManager GM;
    
    void Awake()
    {
        GM = gameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;
    }
    
    void Start()
    {
        GM.SetGameState(GameState.SCORE);
        Debug.Log("Current game state: " + GM.gameState);
    }
    
    public void HandleOnStateChange()
    {
        Debug.Log("OnStateChange!");
    }

    public void loadLevel()
    {
        sceneManager.GoToLevel1Screen();
    }
}
