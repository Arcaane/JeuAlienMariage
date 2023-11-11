using System.Collections;
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

    public async void SetDate(int date)
    {
        GameManager.Instance.SetDate(date);

        dateOnMatch.sprite = dateSprite[date - 1];
        playerOnMatch.sprite = playersSprite[GameManager.Instance.players.Count - 1];

        matchImage.gameObject.SetActive(true);
        sliders[0].DOValue(1, 0.5f).Play();
        sliders[1].DOValue(1, 0.5f).Play().OnComplete(
            () => matchImage.transform.DOScale(1f, 1f).OnComplete(
                () => matchImage.transform.DOPunchPosition(Vector3.one, 0.2f).OnComplete(
                    () => SceneManager.LoadScene(1))));

        await Task.Delay(500);
        GameManager.Instance.uiManager.ActivateNextPhase();
    }
}
