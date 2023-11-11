using System;
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

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        uiManager.SetPhaseIndex(0);
    }
    public void SetNextPlayer()
    {
        currentIndex = currentIndex >= players.Count - 1 ? 0 : currentIndex+1;
        activePlayer = players[currentIndex];
    }

    public void SetupPlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            Player newPlayer = new Player();
            newPlayer.SetIndex(i);
            newPlayer.ResetScore();
            players.Add(newPlayer);
        }

        activePlayer = players[0];
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
    
}
