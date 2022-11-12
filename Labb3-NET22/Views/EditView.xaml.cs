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
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        private QuizManager _quizManager = new();

        private Question SelectedQuestion { get; set; }
        public bool radioButtonisChecked = false;
        private int CorrectAnswer { get; set; }
        private int SelectedQuestionIndex { get; set; }
        
        public EditView()
        {
            InitializeComponent();
            Task.Run(()=>_quizManager.CheckForQuizes());
            QuizBox.ItemsSource = _quizManager.QuizList;
        }

        public void EmptyTextFields()
        {
            Question.Text = String.Empty;
            Answer1.Text = String.Empty;
            Answer2.Text = String.Empty;
            Answer3.Text = String.Empty;
            Answer4.Text = String.Empty;
        }

        private void FinaliseAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonisChecked = true;
            RadioButton radioButton = e.Source as RadioButton;

            foreach (var c in radioButton.Name)
            {
                if (char.IsDigit(c))
                {
                    CorrectAnswer = c - 48;
                }
            }
        }

        private void QuizBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteQuizButton.IsEnabled = true;
            if (QuizBox.SelectedItem is Quiz)
            {
                var quiz = QuizBox.SelectedItem as Quiz;
                _quizManager.CurrentQuiz = quiz;
                Title.Text = _quizManager.CurrentQuiz.ToString();
                QuestionBox.ItemsSource = _quizManager.CurrentQuiz.Questions;
            }
        }

        private void QuestionBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionBox.SelectedItem is not DataModels.Question) return;
            var question = QuestionBox.SelectedItem as Question;
            Question.Text = question.Statement;
            Answer1.Text = question.Answers[0];
            Answer2.Text = question.Answers[1];
            Answer3.Text = question.Answers[2];
            Answer4.Text = question.Answers[3];
            SelectedQuestion = question;
            SelectedQuestionIndex = QuestionBox.SelectedIndex;
            int elementOfRadioButton;
            List<RadioButton> radioButtons = new List<RadioButton>()
                { RadioButton0, RadioButton1, RadioButton2, RadioButton3 };
            foreach (var radioButton in radioButtons.Where(radioButton => radioButton.Name.Contains(question.CorrectAnswer.ToString())))
            {
                elementOfRadioButton = radioButtons.IndexOf(radioButton);
                switch (elementOfRadioButton)
                {
                    case 0:
                        RadioButton0.IsChecked = true;
                        break;
                    case 1:
                        RadioButton1.IsChecked = true;
                        break;
                    case 2:
                        RadioButton2.IsChecked = true;
                        break;
                    case 3:
                        RadioButton3.IsChecked = true;
                        break;
                }
            }
            RemoveQuestion.IsEnabled = true;
            UpdateQuestion.IsEnabled = true;

        }

        private void RemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            _quizManager.CurrentQuiz.RemoveQuestion(QuestionBox.SelectedIndex);
            _quizManager.SaveQuiz();
            QuestionBox.ItemsSource = _quizManager.CurrentQuiz.Questions;
            EmptyTextFields();
            RemoveQuestion.IsEnabled = false;
            UpdateQuestion.IsEnabled = false;
        }

        private void UpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "" || Question.Text == "" || !radioButtonisChecked || Answer1.Text == "" ||
                Answer2.Text == "" || Answer3.Text == "" || Answer4.Text == "") return;
            SelectedQuestion = new Question(Question.Text, CorrectAnswer, Answer1.Text, Answer2.Text, Answer3.Text,
                Answer4.Text);
            _quizManager.CurrentQuiz.UpdateQuestion(SelectedQuestion, SelectedQuestionIndex);
            _quizManager.SaveQuiz();
            QuestionBox.ItemsSource = _quizManager.CurrentQuiz.Questions;
        }

        private void DeleteQuizButton_OnClick(object sender, RoutedEventArgs e)
        {
            _quizManager.DeleteQuiz();
            _quizManager.QuizList.Remove(_quizManager.CurrentQuiz);
            QuizBox.Items.Refresh();
            QuestionBox.ItemsSource = null;
            EmptyTextFields();
            DeleteQuizButton.IsEnabled = false;
        }
    }
}
