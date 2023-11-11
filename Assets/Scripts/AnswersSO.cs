using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Answer", menuName = "Dialogue/Answer")]
public class AnswersSO : ScriptableObject
{
    public string sentence;
    [TextArea(5, 10)] public string answerDescription;
    public Sprite answersSprite;
    
    public List<Traits> answersTraits = new();
    public List<Consequences> answersConsequences = new ();

    [ContextMenu("Setup")]
    public void Setup()
    {
        Debug.Log(Enum.GetNames(typeof(Traits)).Length);
        for (int i = 0; i < Enum.GetNames(typeof(Traits)).Length; i++)
        {
            answersTraits.Add((Traits)i);
            answersConsequences.Add((Consequences)Random.Range(0, Enum.GetNames(typeof(Traits)).Length));
        }
    }
}
