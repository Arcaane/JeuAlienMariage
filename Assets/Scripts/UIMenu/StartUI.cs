using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject PlayernbUI;
    
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            PlayernbUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
