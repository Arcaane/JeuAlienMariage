using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject choosePlayerCountUI;
    [SerializeField] private GameObject chooseCharacterUI;
    [SerializeField] private GameObject chooseTargetUI;
    //[SerializeField] private GameObject inGameUIHandler;
    [SerializeField] private GameObject background;

    private void Start()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private int currentPhaseIndex;

    public void ActivatePhase()
    {
        Debug.Log(currentPhaseIndex);
        switch (currentPhaseIndex)
        {
            case 0:
                DeactivateUI();
                startUI.SetActive(true);
                break;
            case 1:
                DeactivateUI();
                choosePlayerCountUI.SetActive(true);
                break;
            case 2:
                DeactivateUI();
                chooseCharacterUI.SetActive(true);
                break;
            case 3:
                DeactivateUI();
                chooseTargetUI.SetActive(true);
                break;
            case 4:
                DeactivateUI();
                //inGameUIHandler.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void DeactivateUI()
    {
        startUI.SetActive(false);
        choosePlayerCountUI.SetActive(false);
        chooseCharacterUI.SetActive(false);
        chooseTargetUI.SetActive(false);
    }

    public void SetPhaseIndex(int index)
    {
        currentPhaseIndex = index;
        ActivatePhase();
    }

    public void ActivateNextPhase()
    {
        currentPhaseIndex++;
        ActivatePhase();
    }

    public void ActivatePreviousPhase()
    {
        currentPhaseIndex--;
        ActivatePhase();
    }
}