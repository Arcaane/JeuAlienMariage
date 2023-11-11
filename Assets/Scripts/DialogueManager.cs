using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public List<QuestionsSO> questionPull;
    public int answersCount = 0;
    [SerializeField] private QuestionsSO tempQuestion;
    
    [Header("PlayerParam")] 
    public bool canAnswer;
    public QuestionsSO startDialogue;
    
    [Header("Text Apparition Param")]
    public float questionsApparitionSpeed = 0.1f;
    public float answerApparitionSpeed = 0.05f;
    private TextMeshProUGUI questionText;
    private string tempText;
    private int indexLettre = 0;
    private float tempsEcoule;
    private float timer = 0;

    [Header("UI Attributes")] 
    public GameObject[] answerContainers;
    public TextMeshProUGUI[] answerText;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        tempQuestion = startDialogue;
        AskQuestion(tempQuestion);
    }

    private async void AskQuestion(QuestionsSO question = null)
    {
        answersCount = tempQuestion.reponsesPossibles.Length;
        await AppearText(tempQuestion.questionSentence, questionsApparitionSpeed);
        for (int i = 0; i < answersCount; i++)
        {
            await AppearText(tempQuestion.reponsesPossibles[i].sentence, answerApparitionSpeed);
        }

        canAnswer = true;
    }

    private async Task AppearText(string text, float textSpeed)
    {
        indexLettre = 0;
        if (indexLettre < questionText.text.Length)
        {
            await Task.Yield();
            timer += Time.deltaTime;
            
            if (timer > textSpeed)
            {
                timer = 0;
                tempText += questionText.text[indexLettre];
                questionText.text = tempText;
                indexLettre++;
            }
        }
    }

    private void SelectQuestion()
    {
        
    }

    #region Input
    public void OnXButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DoAnswerA();
        }
    }

    private void DoAnswerA()
    {
        canAnswer = false;
        Debug.Log("Player Answer A");
    }

    public void OnYButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DoAnwserY();
        }
    }

    private void DoAnwserY()
    {
        canAnswer = false;
        Debug.Log("Player Answer Y");
    }

    public void OnBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DoAnwserB();
            Debug.Log("Player Answer B");
        }
    }

    private void DoAnwserB()
    {
        canAnswer = false;
    }

    public void OnAButton(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DoAnwserA();
            Debug.Log("Player Answer A");
        }
    }

    private void DoAnwserA()
    {
        canAnswer = false;
    }
    #endregion
}
