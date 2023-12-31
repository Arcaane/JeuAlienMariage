using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterUI : MonoBehaviour
{
    [SerializeField] private Buttons[] buttons;
    [SerializeField] private TextMeshProUGUI titleText;
    private TextMeshPro text;
    
    [Serializable]
    public struct Buttons
    {
        public Button button;
        public Image buttonBackground;
        public TextMeshProUGUI text;
        public Sprite sprite;
        public Sprite kissSprite;
        public bool spriteSet;
    }

    private void OnEnable()
    {
        foreach (var button in buttons)
        {
            button.text.text = null;
            button.button.enabled = true;
        }
    }

    public void SetPlayerToSprite(int index)
    {
        var button = buttons[index];
        if(button.spriteSet) return;
        var text = button.text;
        var player = GameManager.Instance.GetActivePlayer();
        text.text = "Joueur " + player.GetIndex() + 1;
        player.SetSprite(button.sprite);
        player.SetKissSprite(button.kissSprite);
        buttons[index].buttonBackground.color = GameManager.Instance.GetActiveColor();
        buttons[index].spriteSet = true;
        buttons[index].button.enabled = false;
        NextPlayer();
    }

    private bool CheckButtons()
    {
        var playerValidateCount = 0;
        
        foreach (var button in buttons)
        {
            if (button.spriteSet) playerValidateCount++;
        }
        
        return playerValidateCount == GameManager.Instance.players.Count;
    }
    
    private void NextPlayer()
    {
        if (CheckButtons())
        {
            GameManager.Instance.uiManager.ActivateNextPhase();
        }
        GameManager.Instance.SetNextPlayer();
        int playerIndex = GameManager.Instance.GetActivePlayer().GetIndex();
        titleText.text = "Joueur " + playerIndex + ", choisis un personnage";
    }
    
}
