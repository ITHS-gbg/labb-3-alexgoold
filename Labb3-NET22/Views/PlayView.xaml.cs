using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for PlayView.xaml
    /// </summary>
    public partial class PlayView : UserControl
    {
        QuizManager _quizManager = new QuizManager();
        private Question ActiveQuestion { get; set; }
        private int Score { get; set; }
        private int QuestionNumber { get; set; }
        private int TotalQuestions { get; set; }
        public PlayView()
        {
            InitializeComponent();

            _quizManager.CurrentQuiz = new Quiz("bigQuiz");

            _quizManager.LoadQuiz();

            ActiveQuestion = _quizManager.CurrentQuiz.GetRandomQuestion();
            TotalQuestions = _quizManager.CurrentQuiz.Questions.Count();
            QuestionNumber = 1;
            Score = 0;
            ScoreText.Text = Score.ToString();
            UpdateQuestionAndAnswers();
            
        }

        public void UpdateQuestionAndAnswers()
        {
            QuestionNumberInfo.Text = $"Question {QuestionNumber} of {TotalQuestions}";
            CorrectOrNot.Text = String.Empty;
            Answer1.Foreground = new SolidColorBrush(Colors.Black);
            Answer2.Foreground = new SolidColorBrush(Colors.Black);
            Answer3.Foreground = new SolidColorBrush(Colors.Black);
            Answer4.Foreground = new SolidColorBrush(Colors.Black);
            QuestionText.Text = ActiveQuestion.Statement;
            Answer1.Text = ActiveQuestion.Answers[0];
            Answer2.Text = ActiveQuestion.Answers[1];
            Answer3.Text = ActiveQuestion.Answers[2];
            Answer4.Text = ActiveQuestion.Answers[3];
        }

        public void Button_OnClick(object sender, RoutedEventArgs e)
        {

            Button button = e.Source as Button;

            if (CorrectOrNot.Text == "\nWrong!" || CorrectOrNot.Text == "\nCorrect!")
            {

            }
            else if (button.Name.Contains(ActiveQuestion.CorrectAnswer.ToString()) && CorrectOrNot.Text != "\nCorrect!")
            {
                CorrectOrNot.Foreground = new SolidColorBrush(Colors.Green);
                CorrectOrNot.Text = "\nCorrect!";
                Score++;
                ScoreText.Text = Score.ToString();
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
                QuestionText.Foreground = new SolidColorBrush(Colors.Red);
                QuestionText.Text = "NO MORE QUESTIONS!";
            }
        }

    }
}
