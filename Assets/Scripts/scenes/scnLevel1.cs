using UnityEngine;
using System.Collections;

public class scnLevel1 : MonoBehaviour
{
    gameManager GM;
    
    void Awake()
    {
        GM = gameManager.Instance;
        print ("HERE: " + gameManager.Instance) ;
        GM.OnStateChange += HandleOnStateChange;
    }

    void Start () {
        print ("GM: " + GM);
        GM.SetGameState(GameState.LEVEL1);
        Debug.Log("Current game state: " + GM.gameState);
    }
    
    public void HandleOnStateChange()
    {
        Debug.Log("OnStateChange!");
    }
}
