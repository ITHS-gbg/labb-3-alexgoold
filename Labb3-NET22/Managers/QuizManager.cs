using Labb3_NET22.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    
    public Quiz CurrentQuiz { get; set; }


    public void LoadQuiz()
    {

        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CurrentQuiz.Title+".json");

        string jsonstring = File.ReadAllText(FilePath);

        CurrentQuiz = JsonConvert.DeserializeObject<Quiz>(jsonstring);

    }
}