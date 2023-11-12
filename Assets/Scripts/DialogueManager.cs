using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    
    
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.uiManager.ActivateNextPhase();
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
        Debug.Log("text affiché");

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

    int CalculateAnswerRate(List<Traits> dateTraits, List<Traits> answerTraits, List<Consequences> answerConsequence)
    {
        int rate = 0;
        foreach (var dTrait in dateTraits)
        {
            for (int i = 0; i < answerTraits.Count; i++)
            {
                if (answerTraits[i] == dTrait)
                {
                    switch (answerConsequence[i])
                    {
                        case Consequences.Positive:
                            rate += 1;
                            break;
                        case Consequences.Negative:
                            rate -= 1;
                            break;
                        case Consequences.VeryNegative:
                            rate -= 2;
                            break;
                        case Consequences.VeryPositive:
                            rate += 2;
                            break;
                    }
                }
            }
        }

        if (rate > 2) rate = 2;
        if (rate < -2) rate = -2;

        Debug.Log(rate);
        return rate;
    }

    public void GetRandomQuestion()
    {
        if (currentDateQuestions.Count <= 0) SetupQuestions();
        foreach (var question in currentDateQuestions)
        {
            Debug.Log(question);
        }
        var rand = Random.Range(0, currentDateQuestions.Count);
        tempQuestion = currentDateQuestions[rand];
        currentDateQuestions.RemoveAt(rand);
        AskQuestion(tempQuestion);
    }

    #region Input

    private async void DoAnswerX()
    {
        canAnswer = false;
        Debug.Log("Player Answer X");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[0].answersTraits,
            tempQuestion.reponsesPossibles[0].answersConsequences);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[0].answerDescription, answerApparitionSpeed, questionText);
        await Task.Delay(450);
        GetRandomQuestion();
    }

    private async void DoAnswerY()
    {
        canAnswer = false;
        Debug.Log("Player Answer Y");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[1].answersTraits,
            tempQuestion.reponsesPossibles[1].answersConsequences);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[1].answerDescription, answerApparitionSpeed, questionText);
        await Task.Delay(450);
        GetRandomQuestion();
    }

    private async void DoAnswerB()
    {
        canAnswer = false;
        Debug.Log("Player Answer B");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[2].answersTraits,
            tempQuestion.reponsesPossibles[2].answersConsequences);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[2].answerDescription, answerApparitionSpeed, questionText);
        await Task.Delay(450);
        GetRandomQuestion();
    }

    private async void DoAnswerA()
    {
        canAnswer = false;
        Debug.Log("Player Answer A");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[3].answersTraits,
            tempQuestion.reponsesPossibles[3].answersConsequences);
        CloseAnswersSection();
        await AppearText(tempQuestion.reponsesPossibles[3].answerDescription, answerApparitionSpeed, questionText);
        await Task.Delay(450);
        GetRandomQuestion();
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount == 4)
        {
            DoAnswerA();
        }
    }

    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 3)
        {
            DoAnswerB();
        }
    }

    public void OnYButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 2)
        {
            DoAnswerY();
        }
    }

    public void OnXButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 1)
        {
            DoAnswerX();
        }
    }

    #endregion
}