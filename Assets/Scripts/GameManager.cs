using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public static GameManager Instance;
    public UIManager uiManager;
    
    [Header("Players")]
    public List<Player> players;
    private Player activePlayer;
    private int currentIndex;
    [SerializeField] private Color[] playerColors;
    private int currentPlayerHearts;
    
    [Header("Dates")]
    private Date currentDate;
    [SerializeField] private Dates[] dates;
    private Dictionary<DatesEnum.Dates, Date> datesDictionnary = new Dictionary<DatesEnum.Dates, Date>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Initialize();
        DontDestroyOnLoad(this);
    }

    private void Initialize()
    {
        uiManager.SetPhaseIndex(0);
        foreach (var date in dates)
        {
            datesDictionnary.Add(date.date, date.dateData);
        }
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

    public Date GetCurrentDate()
    {
        return currentDate;
    }

    public void SetDate(int date)
    {
        DatesEnum.Dates chosenDate;
        switch (date)
        {
            case 1 :
                chosenDate = DatesEnum.Dates.Gloup;
                break;
            case 2 :
                chosenDate = DatesEnum.Dates.Gotgotus;
                break;
            case 3 :
                chosenDate = DatesEnum.Dates.Graillax;
                break;
            default:
                return;
                break;
        }
        
        currentDate = datesDictionnary[chosenDate];
    }

    public Color GetActiveColor()
    {
        return playerColors[currentIndex];
    }

    public int GetCurrentHearts()
    {
        return currentPlayerHearts;
    }

    public void AddHeart()
    {
        currentPlayerHearts ++;
    }
    
    [Serializable] 
    public struct Dates
    {
        public DatesEnum.Dates date;
        public Date dateData;
    }
}
