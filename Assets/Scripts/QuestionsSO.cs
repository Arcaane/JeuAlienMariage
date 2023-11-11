using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Dialogue/Question")]
public class QuestionsSO : ScriptableObject
{
    public string questionSentence;
    public AnswersSO[] reponsesPossibles;
}
