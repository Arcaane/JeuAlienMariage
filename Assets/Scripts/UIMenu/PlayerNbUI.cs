using UnityEngine;

public class PlayerNbUI : MonoBehaviour
{

    [SerializeField] private GameObject[] texts;
    
    public void ChooseCount(int nb)
    {
        GameManager.Instance.uiManager.ActivateNextPhase();
        GameManager.Instance.SetupPlayers(nb);
    }
}
