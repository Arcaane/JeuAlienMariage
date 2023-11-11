using System.Collections.Generic;
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
    
    public List<Traits> TraitsList = new ();
    
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

    void CalculateAnswerRate(List<Traits> dateTraits, List<Traits> answerTraits, List<Consequences> answerConsequence)
    {
        
    }
    
    #region Input

    private void DoAnswerX()
    {
        canAnswer = false;
        Debug.Log("Player Answer X");
        
        CloseAnswersSection();
    }

    private void DoAnwserY()
    {
        canAnswer = false;
        CloseAnswersSection();
        Debug.Log("Player Answer Y");
    }

    private void DoAnwserB()
    {
        canAnswer = false;
        CloseAnswersSection();
        Debug.Log("Player Answer B");
    }

    private void DoAnwserA()
    {
        canAnswer = false;
        CloseAnswersSection();
        Debug.Log("Player Answer A");
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer)
        {
            DoAnwserA();
        }
    }

    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer)
        {
            DoAnwserB();
        }
    }

    public void OnYButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer)
        {
            DoAnwserY();
        }
    }

    public void OnXButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canAnswer)
        {
            DoAnswerX();
        }
    }

    #endregion
}