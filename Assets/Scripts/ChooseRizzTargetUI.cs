using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ChooseRizzTargetUI : MonoBehaviour
{
    public Slider[] sliders;
    public Image matchImage;
    public Image playerOnMatch;
    public Image dateOnMatch;
    public Sprite[] playersSprite;
    public Sprite[] dateSprite;
    private int tinderIndex;
    [SerializeField] private List<Sprite> tinderProfiles;
    [SerializeField] private Image currentTinder;

    public async void SetDate()
    {
        GameManager.Instance.SetDate(tinderIndex);

        dateOnMatch.sprite = dateSprite[tinderIndex];
        playerOnMatch.sprite = playersSprite[GameManager.Instance.players.Count - 1];

        matchImage.gameObject.SetActive(true);
        sliders[0].DOValue(1, 0.5f).Play();
        sliders[1].DOValue(1, 0.5f).Play().OnComplete(
            () => matchImage.transform.DOScale(1f, 1f).OnComplete(
                () => matchImage.transform.DOPunchPosition(Vector3.one, 0.2f).OnComplete(
                    () => SceneManager.LoadScene(1))));

        await Task.Delay(2000);
        GameManager.Instance.uiManager.ActivateNextPhase();
    }

    private void DisplayProfile()
    {
        currentTinder.sprite = tinderProfiles[tinderIndex];
    }
    public void NextTinder()
    {
        tinderIndex = tinderIndex >= tinderProfiles.Count - 1 ? tinderProfiles.Count - 1 : tinderIndex+1;
        DisplayProfile();
    }

    public void PreviousTinder()
    {
        tinderIndex = tinderIndex <= 0 ? 0 : tinderIndex-1;
        DisplayProfile();
    }
}
