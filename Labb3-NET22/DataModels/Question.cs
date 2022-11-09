using Newtonsoft.Json;

namespace Labb3_NET22.DataModels;

[JsonObject]
public class Question
{
    public Question(string statement, int correctAnswer, params string[] answers)
    {
        Statement = statement;
        CorrectAnswer = correctAnswer;
        Answers = answers;
    }
    public string Statement { get;}
    public string[] Answers { get;}
    public int CorrectAnswer { get;}

    public override string ToString()
    {
        return Statement;
    }
}