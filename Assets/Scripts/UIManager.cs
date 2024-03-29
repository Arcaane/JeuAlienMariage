using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject choosePlayerCountUI;
    [SerializeField] private GameObject chooseCharacterUI;
    [SerializeField] private GameObject chooseTargetUI;
    //[SerializeField] private GameObject inGameUIHandler;
    
    public EventSystem eventSystem;
    [SerializeField] private GameObject[] firstSelectedByPannels;
    
    [Header("Background")]
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] menuBackground;
    private int currentPhaseIndex;
    public GameObject hideWithHearts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


    public void ActivatePhase()
    {
        switch (currentPhaseIndex)
        {
            case 0:
                DeactivateUI();
                startUI.SetActive(true);
                break;
            case 1:
                DeactivateUI();
                eventSystem.SetSelectedGameObject(firstSelectedByPannels[0]);
                choosePlayerCountUI.SetActive(true);
                break;
            case 2:
                DeactivateUI();
                chooseCharacterUI.SetActive(true);
                eventSystem.SetSelectedGameObject(firstSelectedByPannels[1]);
                break;
            case 3:
                DeactivateUI();
                chooseTargetUI.SetActive(true);
                eventSystem.SetSelectedGameObject(firstSelectedByPannels[2]);
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

    public void SetBackground(Sprite sprite)
    {
        background.sprite = sprite;
    }

    public void ActivateNextPhase()
    {
        currentPhaseIndex++;
        ActivatePhase();
    }

    public void ActivatePreviousPhase(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            currentPhaseIndex--;
            ActivatePhase();
        }
    }
}