using UnityEngine;
using System.Collections;

public class scnTitle : MonoBehaviour
{
    gameManager GM;
    
    void Awake()
    {
        GM = gameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;
    }
    
    void Start()
    {
        GM.SetGameState(GameState.TITLE);
        Debug.Log("Current game state: " + GM.gameState);
    }
    
    public void HandleOnStateChange()
    {
        Debug.Log("OnStateChange!");
    }
}
