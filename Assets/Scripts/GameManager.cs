using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player[] players;
    private Player activePlayer;
    private int currentIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void SetNextPlayer()
    {
        currentIndex = currentIndex >= players.Length - 1 ? 0 : currentIndex+1;
        Debug.Log(players.Length);
        Debug.Log(currentIndex);
        activePlayer = players[currentIndex];
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
    
}
