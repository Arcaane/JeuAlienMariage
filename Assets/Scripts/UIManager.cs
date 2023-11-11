using UnityEngine;

public class UIManager : MonoBehaviour
{
     [SerializeField] private GameObject startUI;
     [SerializeField] private GameObject choosePlayerCountUI;
     [SerializeField] private GameObject chooseCharacterUI;
     [SerializeField] private GameObject chooseTargetUI;

     private int currentPhaseIndex;

     public void ActivatePhase()
     {
          switch (currentPhaseIndex)
          {
               case 0 :
                    DeactivateUI();
                    startUI.SetActive(true);
                    break;
               case 1 :
                    DeactivateUI();
                    choosePlayerCountUI.SetActive(true);
                    break;
               case 2 :
                    DeactivateUI();
                    chooseCharacterUI.SetActive(true);
                    break;
               case 3 :
                    DeactivateUI();
                    chooseTargetUI.SetActive(true);
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
