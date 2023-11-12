using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public Date date;

    [SerializeField] private List<QuestionsSO> currentDateQuestions;
    [SerializeField] private int answersCount = 0;
    [SerializeField] private QuestionsSO tempQuestion;

    [Header("PlayerParam")] public bool canAnswer;
    public QuestionsSO startDialogue;

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

    private int selectedAnswer;
    public bool canSkip;
    
    // Start is called before the first frame update
    void Start()
    {
        date = GameManager.Instance.GetCurrentDate();
        SetupQuestions();
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
        tempQuestion = startDialogue;
        CloseAnswersSection();
        AskQuestion(tempQuestion);
    }
    
    public async Task AskQuestion(QuestionsSO question = null)
    {
        QuestionSection.SetActive(true);
        answersCount = tempQuestion.reponsesPossibles.Length;
        await AppearText(tempQuestion.questionSentence, questionsApparitionSpeed, questionText);

        ShowAnswerSelectionUIElements();
        for (int i = 0; i < answersCount; i++)
        {
            answerContainers[i].SetActive(true);
            await AppearText(tempQuestion.reponsesPossibles[i].sentence, answerApparitionSpeed, answerText[i]);
        }

        canAnswer = true;
    }

    public async Task AppearText(string text, float textSpeed, TextMeshProUGUI TextHandler)
    {
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

    public void GetRandomQuestion()
    {
        if (currentDateQuestions.Count <= 0) SetupQuestions();
        var rand = Random.Range(0, currentDateQuestions.Count);
        tempQuestion = currentDateQuestions[rand];
        currentDateQuestions.RemoveAt(rand);
        AskQuestion(tempQuestion);
    }

    #region Input

    private async void DoAnswer(int index)
    {
        canAnswer = false;
        var a = CalculateAnswerRate(tempQuestion.reponsesPossibles[index].answersConsequences);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[index].answerDescription, answerApparitionSpeed, questionText);
        canSkip = true;
        if(a > 0)GameUIManager.Instance.AddHeart();
        else GameUIManager.Instance.PlayerLost();
        
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (canSkip && ctx.started)
        {
            canSkip = false;
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

    #endregion
}