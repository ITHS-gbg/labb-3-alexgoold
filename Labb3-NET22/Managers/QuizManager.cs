using Labb3_NET22.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    
    public Quiz CurrentQuiz { get; set; }

    public List<Quiz> QuizList = new List<Quiz>();

    public void LoadQuiz()
    {

        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CurrentQuiz.Title);

        string jsonstring = File.ReadAllText(FilePath);

        CurrentQuiz = JsonConvert.DeserializeObject<Quiz>(jsonstring);


    }

    public void SaveQuiz()
    {
        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CurrentQuiz.Title + ".json");

        var jsonstring = JsonConvert.SerializeObject(CurrentQuiz);

        using StreamWriter sw = new StreamWriter(FilePath);

        sw.WriteLine(jsonstring);

    }

    public void CheckForQuizes()
    {
        var localAppFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); 
        var files= Directory.EnumerateFileSystemEntries(localAppFolder, "*.json");
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            CurrentQuiz = new Quiz(fileName);
            var jsonString = File.ReadAllText(file);
            CurrentQuiz = JsonConvert.DeserializeObject<Quiz>(jsonString);
            QuizList.Add(CurrentQuiz);
        }
    }
}