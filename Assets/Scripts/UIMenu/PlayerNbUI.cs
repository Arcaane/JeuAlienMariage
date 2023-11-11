using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNbUI : MonoBehaviour
{
    [SerializeField] private GameObject ChooseCharacterUI;
    [SerializeField] private GameManager GameManager;
    
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            Chose(3);
        }
        else if (Input.GetKeyDown("joystick button 1"))
        {
            Chose(2);
        }
        else if (Input.GetKeyDown("joystick button 2"))
        {
            Chose(4);
        }
        else if (Input.GetKeyDown("joystick button 3"))
        {
            Chose(1);
        }
        
    }

    void Chose(int nb)
    {
        ChooseCharacterUI.SetActive(true);
        gameObject.SetActive(false);
        GameManager.players = new Player[nb];

    }
}
