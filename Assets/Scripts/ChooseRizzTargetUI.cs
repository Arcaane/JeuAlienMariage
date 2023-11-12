using System;
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
    public GameObject matchImage;
    public Image playerOnMatch;
    public Image dateOnMatch;
    public Sprite[] playersSprite;
    public Sprite[] dateSprite;
    private int tinderIndex;
    [SerializeField] private List<Sprite> tinderProfiles;
    [SerializeField] private Image currentTinder;
    [SerializeField] private GameObject UI;

    private void Start()
    {
        matchImage.SetActive(false);
        UI.SetActive(true);
    }

    public async void SetDate()
    {
        GameManager.Instance.SetDate(tinderIndex);

        dateOnMatch.sprite = dateSprite[tinderIndex];
        playerOnMatch.sprite = playersSprite[GameManager.Instance.players.Count - 1];

        matchImage.SetActive(true);

        await Task.Delay(1000);
        UI.SetActive(false);
        SceneManager.LoadScene(1);
        await Task.Delay(3000);
        GameManager.Instance.uiManager.ActivateNextPhase();
    }

    private void DisplayProfile()
    {
        currentTinder.sprite = tinderProfiles[tinderIndex];
    }
    public void NextTinder()
    {
        tinderIndex = tinderIndex >= tinderProfiles.Count - 1 ? 0 : tinderIndex+1;
        DisplayProfile();
    }

    public void PreviousTinder()
    {
        tinderIndex = tinderIndex <= 0 ? tinderProfiles.Count - 1: tinderIndex-1;
        DisplayProfile();
    }
}
