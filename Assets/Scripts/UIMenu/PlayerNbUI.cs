using UnityEngine;

public class PlayerNbUI : MonoBehaviour
{

    [SerializeField] private GameObject[] texts;
    
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Z))
        {
            ChooseCount(3);
        }
        else if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Q))
        {
            ChooseCount(2);
        }
        else if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.S))
        {
            ChooseCount(4);
        }
        else if (Input.GetKeyDown("joystick button 3")|| Input.GetKeyDown(KeyCode.D))
        {
            ChooseCount(1);
        }
        
    }
    void ChooseCount(int nb)
    {
        GameManager.Instance.uiManager.ActivateNextPhase();
        GameManager.Instance.SetupPlayers(nb);
    }
}
