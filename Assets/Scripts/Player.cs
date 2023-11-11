using UnityEngine;

[System.Serializable]
public class Player
{
    private int playerIndex;
    private Color color;
    private Sprite playerSprite;
    private int playerScore;

    public int GetIndex()
    {
        return playerIndex;
    }

    public void SetIndex(int index)
    {
        playerIndex = index;
    }

    public void SetSprite(Sprite sprite)
    {
        playerSprite = sprite;
    }

    public Sprite GetSprite()
    {
        return playerSprite;
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
