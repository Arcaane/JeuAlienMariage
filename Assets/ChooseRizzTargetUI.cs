using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRizzTargetUI : MonoBehaviour
{
    public void SetDate(int date)
    {
        GameManager.Instance.SetDate(date);
    }
}
