using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    public Date date;

    [SerializeField] private List<QuestionsSO> currentDateQuestions;
    [SerializeField] private int answersCount = 0;
    [SerializeField] private QuestionsSO tempQuestion;

    [Header("PlayerParam")] public bool canAnswer;

    [Header("Text Apparition Param")] 
    public float questionsApparitionSpeed = 0.1f;
    public float answerApparitionSpeed = 0.05f;
    [SerializeField] public TextMeshProUGUI questionText;
    public string tempText;
    public int indexLettre = 0;
    public float timer = 0;

    [Header("UI Attributes")] 
    public GameObject[] answerContainers;
    public TextMeshProUGUI[] answerText;
    public GameObject hideUI;
    public GameObject AnswerSection;
    public GameObject QuestionSection;
    public GameObject continueButton;
    [SerializeField] private GameObject pauseMenu;

    private int selectedAnswer;
    public bool canSkipDescription;
    public bool canSkipReaction;
    public bool playerLost;
    public Image dateImage;

    public bool isDrawingText;
    
    // Start is called before the first frame update
    void Start()
    {
        hideUI = GameManager.Instance.uiManager.hideWithHearts;
        date = GameManager.Instance.GetCurrentDate();
        SetupQuestions();
        continueButton.SetActive(false);
        timer = 0;
    }

    private void SetupQuestions()
    {
        foreach (var question in date.questions)
        {
            currentDateQuestions.Add(question);
        }
    }
    public void StartDate()
    {
        CloseAnswersSection();
        GetRandomQuestion();
    }
    
    public async Task AskQuestion(QuestionsSO question = null)
    {
        if (isDrawingText) return;
        
        hideUI.SetActive(false);
        isDrawingText = true;
        AudioManager.instance.PlaySoundOnce(7);
        QuestionSection.SetActive(true);
        answersCount = tempQuestion.reponsesPossibles.Length;
        dateImage.sprite = GameManager.Instance.GetCurrentDate().spritesAliens[0];
        
        await AppearText(tempQuestion.questionSentence, questionsApparitionSpeed, questionText);
        
        ShowAnswerSelectionUIElements();
        for (int i = 0; i < answersCount; i++)
        {
            answerContainers[i].SetActive(true);
            await AppearText(tempQuestion.reponsesPossibles[i].sentence, answerApparitionSpeed, answerText[i]);
        }
        
        isDrawingText = false;
        canAnswer = true;
    }

    public async Task AppearText(string text, float textSpeed, TextMeshProUGUI TextHandler)
    {
        Debug.Log("J'affiche un texte");
        continueButton.SetActive(false);
        indexLettre = 0;
        tempText = "";
        TextHandler.text = "";
        
        while (indexLettre < text.Length)
        {
            await Task.Yield();
            timer += Time.deltaTime;

            if (timer > textSpeed)
            {
                timer = 0;
                tempText += text[indexLettre];
                TextHandler.text = tempText;
                indexLettre++;
            }
        }
    }

    void ShowAnswerSelectionUIElements()
    {
        AnswerSection.SetActive(true);
        hideUI.SetActive(true);
    }

    void CloseAnswersSection()
    {
        AnswerSection.gameObject.SetActive(false);
        hideUI.SetActive(false);

        for (int i = 0; i < answerContainers.Length; i++)
        {
            answerContainers[i].SetActive(false);
            answerText[i].text = " ";
        }
    }

    int CalculateAnswerRate(Consequences c)
    {
        if (c == Consequences.Negative) return -1;
        return 1;
    }

    private async void GetRandomQuestion()
    {
        hideUI.SetActive(false);
        if (currentDateQuestions.Count <= 0) SetupQuestions();
        var rand = Random.Range(0, currentDateQuestions.Count);
        tempQuestion = currentDateQuestions[rand];
        currentDateQuestions.RemoveAt(rand);
        await AskQuestion(tempQuestion);
    }

    private async void DisplayReaction()
    {
        if(isDrawingText) return;
        
        AudioManager.instance.PlaySoundOnce(7);
        var a = CalculateAnswerRate(tempQuestion.reponsesPossibles[selectedAnswer].answersConsequences);
        if (a > 0)
        {
            GameUIManager.Instance.AddHeart();
            dateImage.sprite = GameManager.Instance.GetCurrentDate().spritesAliens[1];
        }
        await AppearText(tempQuestion.reponsesPossibles[selectedAnswer].alienReaction, answerApparitionSpeed, questionText);
        if (a < 0)
        {
            GameUIManager.Instance.PlayerLost();
            playerLost = true;
            dateImage.sprite = GameManager.Instance.GetCurrentDate().spritesAliens[2];
        }
        canSkipReaction = true;
        continueButton.SetActive(true);
    }

    public void RestartDialogues()
    {
        CloseAnswersSection();
        tempQuestion = null;
        tempText = "";
        canSkipDescription = false;
        canSkipReaction = false;
        canAnswer = false;
        hideUI.SetActive(false);
        GetRandomQuestion();
    }

    #region Input

    private async void DoAnswer(int index)
    {
        AudioManager.instance.PlaySoundOnce(8);
        canAnswer = false;
        hideUI.SetActive(false);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[index].answerDescription, answerApparitionSpeed, questionText);
        canSkipDescription = true;
        selectedAnswer = index;
        continueButton.SetActive(true);
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if(playerLost) return;
        if (canSkipDescription && ctx.started)
        {
            canSkipDescription = false;
            DisplayReaction();
        }

        if (canSkipReaction && ctx.started)
        {
            canSkipReaction = false;
            GetRandomQuestion();
        }
        
        if (ctx.started && canAnswer && answersCount == 4)
        {
            DoAnswer(3);
        }
    }

    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 3)
        {
            DoAnswer(2);
        }
    }

    public void OnYButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 2)
        {
            DoAnswer(1);
        }
    }

    public void OnXButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 1)
        {
            DoAnswer(0);
        }
    }

    public void OnStartButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    #endregion
}