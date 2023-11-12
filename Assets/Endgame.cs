using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Endgame : MonoBehaviour
{

    [SerializeField] private Image alienImage;
    [SerializeField] private Image playerImage;
    [SerializeField] private Sprite[] alienSprites;
    [SerializeField] private Sprite[] alienKissSprite;
    [SerializeField] private Image kissPlayerImage;
    [SerializeField] private Image kissAlienImage;
    [SerializeField] private GameObject endScreen;
    public void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerImage.sprite = GameManager.Instance.GetActivePlayer().GetSprite();
        alienImage.sprite = alienSprites[GameManager.Instance.GetCurrentDate().alienIndex];
        kissAlienImage.sprite = alienKissSprite[GameManager.Instance.GetCurrentDate().alienIndex];
        kissPlayerImage.sprite = GameManager.Instance.GetActivePlayer().GetKissSprite();
        Animation();
    }

    private async void Animation()
    {
        await Task.Delay(3000);
        playerImage.gameObject.SetActive(false);
        alienImage.gameObject.SetActive(false);
        endScreen.SetActive(true);
    }
}
