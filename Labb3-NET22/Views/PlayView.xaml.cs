using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for PlayView.xaml
    /// </summary>
    public partial class PlayView : UserControl
    {
        private QuizManager _quizManager = new();
        private Question ActiveQuestion { get; set; }
        private int Score { get; set; }
        private int QuestionNumber { get; set; }
        private int TotalQuestions { get; set; }
        public PlayView()
        {
            InitializeComponent();
            
        }

        public void EnableButtons()
        {
            NextQuestion.IsEnabled = true;
            Option0.IsEnabled = true;
            Option1.IsEnabled = true;
            Option2.IsEnabled = true;
            Option3.IsEnabled = true;
        }
        public void SetInitialQuiz()
        {
            if (_quizManager.CurrentQuiz.Questions.Any())
            {
                ActiveQuestion = _quizManager.CurrentQuiz.GetRandomQuestion();
                TotalQuestions = _quizManager.CurrentQuiz.Questions.Count();
                QuestionNumber = 1;
                Score = 0;
                ScoreText.Text = Score.ToString();
                UpdateQuestionAndAnswers();
            }
            else
            {
                MessageBox.Show("There are no questions in that quiz! Try another!", "No Questions",
                    MessageBoxButton.OK);
            }
        }

        public void UpdateQuestionAndAnswers()
        {
            EnableButtons();
            QuestionNumberInfo.Text = $"Question {QuestionNumber} of {TotalQuestions}";
            CorrectOrNot.Text = String.Empty;
            Answer1.Foreground = new SolidColorBrush(Colors.Black);
            Answer2.Foreground = new SolidColorBrush(Colors.Black);
            Answer3.Foreground = new SolidColorBrush(Colors.Black);
            Answer4.Foreground = new SolidColorBrush(Colors.Black);
            QuestionText.Foreground = new SolidColorBrush(Colors.Black);
            QuestionText.Text = ActiveQuestion.Statement;
            Answer1.Text = ActiveQuestion.Answers[0];
            Answer2.Text = ActiveQuestion.Answers[1];
            Answer3.Text = ActiveQuestion.Answers[2];
            Answer4.Text = ActiveQuestion.Answers[3];
        }

        public void Button_OnClick(object sender, RoutedEventArgs e)
        {

            Button? button = e.Source as Button;

            if (CorrectOrNot.Text == "\nWrong!" || CorrectOrNot.Text == "\nCorrect!")
            {

            }
            else if (button.Name.Contains(ActiveQuestion.CorrectAnswer.ToString()) && CorrectOrNot.Text != "\nCorrect!")
            {
                CorrectOrNot.Foreground = new SolidColorBrush(Colors.Green);
                CorrectOrNot.Text = "\nCorrect!";
                Score++;
                ScoreText.Text = $"{Score}/{TotalQuestions}";
            }
            else
            {
                CorrectOrNot.Foreground = new SolidColorBrush(Colors.Red);
                CorrectOrNot.Text +="\nWrong!";
            }

        }


        private void NextQuestion_OnClick(object sender, RoutedEventArgs e)
        {
            if (_quizManager.CurrentQuiz.Questions.Count() > 1)
            {
                _quizManager.CurrentQuiz.RemoveQuestion(_quizManager.CurrentQuiz.ElementOfRandomQuestion);
                ActiveQuestion = _quizManager.CurrentQuiz.GetRandomQuestion();
                QuestionNumber++;
                UpdateQuestionAndAnswers();
            }
            else
            {
                var quizFinshedText = $"Quiz finished\nYou scored {Score}/{TotalQuestions}";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(quizFinshedText, "Results", button);
                if (_quizManager.QuizList.Count() == 1)
                {
                    LoadRandomQuizButton.IsEnabled = false;
                }
            }
        }

        private async void LoadQuizFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Filter = "Json Files (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {

                var FilePath = openFileDialog.FileName;

                var fileName = System.IO.Path.GetFileName(FilePath);
                _quizManager.CurrentQuiz = new Quiz(fileName);

                var jsonstring = await File.ReadAllTextAsync(FilePath);

                _quizManager.CurrentQuiz = JsonConvert.DeserializeObject<Quiz>(jsonstring, settings);
                SetInitialQuiz();
            }
        }

        private void LoadDefaultQuizButton_Click(object sender, RoutedEventArgs e)
        {
            _quizManager.CurrentQuiz = _quizManager.CreateDefaultQuiz();
            SetInitialQuiz();
        }
    }
}
