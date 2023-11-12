using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    [SerializeField] private RectTransform dateTr;
    [SerializeField] private RectTransform textBox;
    [SerializeField] private GameObject answerSection;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private RectTransform playerAnouncementTransform;
    [SerializeField] private TextMeshProUGUI playerAnouncementText;
    [SerializeField] private PlayerScreen playerScreen;
    [SerializeField] private GameObject losingScreen;

    public string startText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        dialogueManager.hideUI.SetActive(false);
        playerAnouncementTransform.gameObject.SetActive(true);
        dateTr.position = new Vector3(0, -1080, 0);
        textBox.gameObject.SetActive(false);
        answerSection.SetActive(false);
        dateTr.DOAnchorPos(new Vector3(-375, -20, 0), 0.85f);
        Task.Delay(250);
        textBox.gameObject.SetActive(true);
        LaunchText();
    }

    private async void LaunchText()
    {
        await dialogueManager.AppearText(startText, dialogueManager.questionsApparitionSpeed, dialogueManager.questionText);
        await Task.Delay(2000);
        textBox.gameObject.SetActive(false);
        await AnimDisplayPlayerTurn();
        await Task.Delay(500);
        dialogueManager.StartDate();
    }

    private async Task AnimDisplayPlayerTurn()
    {
        Debug.Log("AnimDisplayPlayerTurn");
        dialogueManager.hideUI.SetActive(true);
        playerAnouncementText.text = $"Player {GameManager.Instance.GetActivePlayer().GetIndex() + 1} turn";
        playerAnouncementTransform.DOAnchorPosX(1920, 0.345f);
        await Task.Delay(3500);
        playerAnouncementTransform.DOAnchorPosX( 0, 0.350f);
        dialogueManager.hideUI.SetActive(false);
        playerAnouncementTransform.gameObject.SetActive(false);
    }

    public void AddHeart()
    {
        GameManager.Instance.AddHeart();
        var index = GameManager.Instance.GetCurrentHearts();
        playerScreen.hearts[index].SetActive(true);
    }

    public async void PlayerLost()
    {
        losingScreen.SetActive(true);
        GameManager.Instance.SetNextPlayer();
        await AnimDisplayPlayerTurn();
    }
    
    
}
