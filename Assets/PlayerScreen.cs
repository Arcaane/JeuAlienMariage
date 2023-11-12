using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScreen : MonoBehaviour
{
    public GameObject[] hearts;
    private Image image;

    public void SetupImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
