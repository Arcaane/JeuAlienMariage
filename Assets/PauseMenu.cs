using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;
    private void OnEnable()
    {
        GameManager.Instance.uiManager.eventSystem.SetSelectedGameObject(firstButton);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Options()
    {
        
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
