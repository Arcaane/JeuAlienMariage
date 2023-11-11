using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public Date date;
    
    [SerializeField] private List<QuestionsSO> questionPoll;
    [SerializeField] private int answersCount = 0;
    [SerializeField] private QuestionsSO tempQuestion;
    
    [Header("PlayerParam")] 
    public bool canAnswer;
    public QuestionsSO startDialogue;
    
    [Header("Text Apparition Param")]
    public float questionsApparitionSpeed = 0.1f;
    public float answerApparitionSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI questionText;
    public string tempText;
    public int indexLettre = 0;
    public float timer = 0;

    [Header("UI Attributes")] 
    public GameObject[] answerContainers;
    public TextMeshProUGUI[] answerText;
    public GameObject hideUI;
    public GameObject answerSelection;

    private int selectedAnswer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        tempQuestion = startDialogue;
        CloseAnswersSection();
        AskQuestion(tempQuestion);
    }

    private async void AskQuestion(QuestionsSO question = null)
    {
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

    private async Task AppearText(string text, float textSpeed, TextMeshProUGUI TextHandler)
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
        answerSelection.SetActive(true);
        hideUI.SetActive(true);
    }
    
    void CloseAnswersSection()
    {
        answerSelection.SetActive(false);
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
                    Debug.Log("Trait: " + answerTraits[i]); // Loyal
                    Debug.Log("Rate Trait: " + answerConsequence[i]); // Loyal

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
        
        return rate;
    }
    
    #region Input

    private void DoAnswerX()
    {
        canAnswer = false;
        Debug.Log("Player Answer X");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[0].answersTraits, tempQuestion.reponsesPossibles[0].answersConsequences);
        CloseAnswersSection();
    }

    private void DoAnwserY()
    {
        canAnswer = false;
        Debug.Log("Player Answer Y");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[1].answersTraits, tempQuestion.reponsesPossibles[1].answersConsequences);
        CloseAnswersSection();
    }

    private void DoAnwserB()
    {
        canAnswer = false;
        Debug.Log("Player Answer B");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[2].answersTraits, tempQuestion.reponsesPossibles[2].answersConsequences);
        CloseAnswersSection();
        
    }

    private void DoAnwserA()
    {
        canAnswer = false;
        Debug.Log("Player Answer A");
        var a = CalculateAnswerRate(date.dateTraits, tempQuestion.reponsesPossibles[3].answersTraits, tempQuestion.reponsesPossibles[3].answersConsequences);
        CloseAnswersSection();
        
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount == 4)
        {
            DoAnwserA();
        }
    }

    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 3)
        {
            DoAnwserB();
        }
    }

    public void OnYButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer && answersCount >= 2)
        {
            DoAnwserY();
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