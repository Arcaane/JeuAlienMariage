using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerIndex;
    private Color color;
    private Sprite sprite;
    private int playerScore;

    public int GetIndex()
    {
        return playerIndex;
    }

    public void SetIndex(int index)
    {
        playerIndex = index;
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void RemoveScore(int score)
    {
        playerScore -= score;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    public bool CheckPoints()
    {
        return playerScore >= 100;
    }
    
}
