using UnityEngine;
using System.Collections;

public class scnLevel1 : MonoBehaviour
{
    gameManager GM;
    
    void Awake()
    {
        GM = gameManager.Instance;
        GM.OnStateChange += HandleOnStateChange;
    }

    void Start () {
        GM.SetGameState(GameState.LEVEL1);
        Debug.Log("Current game state: " + GM.gameState);
    }
    
    public void HandleOnStateChange()
    {
        Debug.Log("OnStateChange!");
    }
}
