using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Player> players;
    private Player activePlayer;
    private int currentIndex;
    [SerializeField] private Sprite[] playerSprites;
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void SetNextPlayer()
    {
        currentIndex = currentIndex >= players.Count ? 0 : currentIndex+1;
        Debug.Log(players.Count);
        Debug.Log(currentIndex);
        activePlayer = players[currentIndex];
    }

    public void SetupPlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            Player newPlayer = new Player();
            newPlayer.SetIndex(i);
            newPlayer.ResetScore();
            newPlayer.SetSprite(playerSprites[i]);
            players.Add(newPlayer);
        }
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
    
}
