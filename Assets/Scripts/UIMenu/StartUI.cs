using UnityEngine;

public class StartUI : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.uiManager.ActivateNextPhase();
        }
    }
}
