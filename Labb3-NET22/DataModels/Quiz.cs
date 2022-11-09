using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace Labb3_NET22.DataModels;

[JsonObject]
public class Quiz
{
    private IEnumerable<Question> _questions;
    private string _title = string.Empty;
    public IEnumerable<Question> Questions => _questions;
    public string Title => _title;
    public int ElementOfRandomQuestion { get; set; }
    public Quiz()
    {
        _questions = new List<Question>();
    }

    public Quiz(string title)
    {
        _title = title;
        _questions = new List<Question>();
    }

    public Question GetRandomQuestion()
    {
        int randomElement = new Random().Next(0, _questions.Count());
        var randomQuestion = Questions.ElementAt(randomElement);
        ElementOfRandomQuestion = randomElement;
        return randomQuestion;
    }
    public void AddQuestion(string statement, int correctAnswer, params string[] answers)
    {
        var newQuestion = new Question(statement,correctAnswer,answers);
        _questions = _questions.Append(newQuestion);
    }

    public void AddQuestion(Question question)
    {
        _questions = _questions.Append(question);
    }
    public void RemoveQuestion(int index)
    {
        var questionToRemove = _questions.ElementAt(index);

        var removedQuestions = _questions.Where(q => q != questionToRemove);

        _questions = removedQuestions;
    }

    public override string ToString()
    {
        if (Title.Contains(".json"))
        {
            var title = Title.Split('.');
            return title[0];
        }
        return Title;
    }
}