using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Answer", menuName = "Dialogue/Answer")]
public class AnswersSO : ScriptableObject
{
    public string sentence;
    [TextArea(5, 10)] public string answerDescription;

    public Consequences answersConsequences;
    
    [TextArea(5, 10)] public string alienReaction;

}
