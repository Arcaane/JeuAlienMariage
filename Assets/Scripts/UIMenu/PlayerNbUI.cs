using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNbUI : MonoBehaviour
{
    [SerializeField] private GameObject chooseCharacterUI;
    
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Z))
        {
            Choose(3);
        }
        else if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Q))
        {
            Choose(2);
        }
        else if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.S))
        {
            Choose(4);
        }
        else if (Input.GetKeyDown("joystick button 3")|| Input.GetKeyDown(KeyCode.D))
        {
            Choose(1);
        }
        
    }

    void Choose(int nb)
    {
        chooseCharacterUI.SetActive(true);
        gameObject.SetActive(false);
        GameManager.Instance.SetupPlayers(nb);

    }
}
