using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScreen : MonoBehaviour
{
    public GameObject[] hearts;
    [SerializeField] private Image image;
    [SerializeField] private Image screenImage;

    public void SetupCharacterImage(Sprite sprite)
    {
        screenImage.sprite = sprite;
    }
    
    public void SetupImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
