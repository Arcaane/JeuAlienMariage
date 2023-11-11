using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform dateTr;
    [SerializeField] private RectTransform textBox;
    [SerializeField] private GameObject answerSection;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private RectTransform playerAnouncementTransform;
    [SerializeField] private TextMeshProUGUI playerAnouncementText;

    public string startText;
    
    // Start is called before the first frame update
    private void Start()
    {
        dialogueManager.hideUI.SetActive(false);
        playerAnouncementTransform.gameObject.SetActive(true);
        dateTr.position = new Vector3(0, -1080, 0);
        textBox.gameObject.SetActive(false);
        answerSection.SetActive(false);
        dateTr.DOAnchorPos(new Vector3(0, -20, 0), 0.85f);
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
}
