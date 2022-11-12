using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Labb3_NET22.Managers;

public class QuizManager
{
    
    public Quiz CurrentQuiz { get; set; }

    public List<Quiz> QuizList = new();

    public void LoadQuiz()
    {

        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+ @"\Quizes\"+ CurrentQuiz.Title);

        string jsonstring = File.ReadAllText(FilePath);

        CurrentQuiz = JsonConvert.DeserializeObject<Quiz>(jsonstring);


    }

    public async void SaveQuiz()
    {
        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Quizes\" + CurrentQuiz.Title + ".json");

        if (CurrentQuiz.Title.Contains(".json"))
        {
            FilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Quizes\" +
                             CurrentQuiz.Title);
        }

        var jsonstring = JsonConvert.SerializeObject(CurrentQuiz);

        using StreamWriter sw = new(FilePath);

        await sw.WriteLineAsync(jsonstring);
        

    }

    public void CheckForQuizes()
    {
        var localAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+ @"\Quizes");
        if (!File.Exists(localAppFolder))
        {
            Directory.CreateDirectory(localAppFolder);
        }
        var files= Directory.EnumerateFileSystemEntries(localAppFolder, "*.json");
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            CurrentQuiz = new Quiz(fileName);
            var jsonString = File.ReadAllText(file);
            var interimQuiz = JsonConvert.DeserializeObject<Quiz>(jsonString);
            foreach (var question in interimQuiz.Questions)
            {
                CurrentQuiz.AddQuestion(question);
            }
            QuizList.Add(CurrentQuiz);
        }
    }

    public void DeleteQuiz()
    {
        var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Quizes\" + CurrentQuiz.Title + ".json");

        if (CurrentQuiz.Title.Contains(".json"))
        {
            FilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Quizes\" +
                             CurrentQuiz.Title);
        }
        File.Delete(FilePath);
        QuizList.Remove(CurrentQuiz);
    }
    public Quiz CreateDefaultQuiz()
    {
        var defaultQuiz = new Quiz();
        var defaultQuizJsonString = @"  { ""Questions"": [
                                        {
                                          ""Statement"": ""How many planets are there?"",
                                          ""Answers"": [ ""4"", ""5"", ""9"", ""8"" ],
                                          ""CorrectAnswer"": 3
                                        },
                                        {
                                          ""Statement"": ""What is a chiuaua"",
                                          ""Answers"": [ ""dog"", ""cast"", ""cat"", ""dast"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""Whats instrument in a piccolo"",
                                          ""Answers"": [ ""woodwind"", ""brass"", ""percussion"", ""electronic"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""Which of the following is not an international organisation?"",
                                          ""Answers"": [ ""Fifa"", ""nato"", ""asean"", ""fbi"" ],
                                          ""CorrectAnswer"": 2
                                        },
                                        {
                                          ""Statement"": ""Which of the following disorders is the fear of being alone?"",
                                          ""Answers"": [ "" Agoraphobia"", ""Aerophobia"", ""Acrophobia"", ""Arachnophobia"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""What year did the first fleet come to Australia?"",
                                          ""Answers"": [ ""1877"", ""1788"", ""1766"", ""1866"" ],
                                          ""CorrectAnswer"": 1
                                        },
                                        {
                                          ""Statement"": ""In 1768, Captain James Cook set out to explore which ocean?"",
                                          ""Answers"": [ ""Pacific Ocean"", ""Atlantic Ocean"", ""Indian Ocean"", ""Arctic Ocean"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""In australian slang, what is a servo?"",
                                          ""Answers"": [ ""A gas station"", ""A waiter"", ""A mechanical component"", ""A marsupial"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""In Australian slang, What is a bottelo?"",
                                          ""Answers"": [ ""A recycling plant"", ""A drunk person"", ""A liqour store"", ""a friendly greeting"" ],
                                          ""CorrectAnswer"": 2
                                        },
                                        {
                                          ""Statement"": ""What is the name of the cantina band in Star Wars?"",
                                          ""Answers"": [ ""Figrin D'an And The Modal Nodes"", ""The Max Rebo Twelve"", ""Evar Orbus and His Galactic Jizz-Wailers"", ""Spem Clunkney and the Cantina Chorus"" ],
                                          ""CorrectAnswer"": 0
                                        },
                                        {
                                          ""Statement"": ""What is the name of the owner of the Mos Eisley cantina in Star Wars"",
                                          ""Answers"": [ ""Wuher"", ""Biggs"", ""Chalmun"", ""Watto"" ],
                                          ""CorrectAnswer"": 2
                                        },
                                        {
                                          ""Statement"": ""What is cynophobia a fear of?"",
                                          ""Answers"": [ ""Poison"", ""Dogs"", ""Baby Geese"", ""Brains"" ],
                                          ""CorrectAnswer"": 1
                                        },
                                        {
                                          ""Statement"": ""Who was the first woman to win a Nobel Prize? "",
                                          ""Answers"": [ ""Bertha von Suttner"", ""Grazia Deledda"", ""Selma Lagerlöf"", ""Marie Curie"" ],
                                          ""CorrectAnswer"": 3
                                        }
                                      ],
                                      ""Title"": ""Default Quiz"",
                                      ""ElementOfRandomQuestion"": 0
                                    }";
        defaultQuiz = JsonConvert.DeserializeObject<Quiz>(defaultQuizJsonString);
        Quiz defaultQuizWithTitle = new("Default Quiz");
        foreach (var question in defaultQuiz.Questions)
        {
            defaultQuizWithTitle.AddQuestion(question);
        }
        return defaultQuizWithTitle;
    }
}