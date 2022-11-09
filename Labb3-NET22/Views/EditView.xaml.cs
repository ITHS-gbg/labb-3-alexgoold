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
        private QuizManager _quizManager = new QuizManager();
        public EditView()
        {
            InitializeComponent();
            _quizManager.CheckForQuizes();
            _quizManager.QuizList.Add(_quizManager.CreateDefaultQuiz());
            QuizBox.ItemsSource = _quizManager.QuizList;
        }

        private void FinaliseAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void QuizBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuizBox.SelectedItem is Quiz)
            {
                var quiz = QuizBox.SelectedItem as Quiz;
                Title.Text = quiz.Title;
                QuestionBox.ItemsSource = quiz.Questions;
            }
            
        }

        private void QuestionBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionBox.SelectedItem is Question)
            {
                var question = QuestionBox.SelectedItem as Question;
                Question.Text = question.Statement;
                Answer1.Text = question.Answers[0];
                Answer2.Text = question.Answers[1];
                Answer3.Text = question.Answers[2];
                Answer4.Text = question.Answers[3];
                int elementOfRadioButton;
                List<RadioButton> radioButtons = new List<RadioButton>()
                    { RadioButton0, RadioButton1, RadioButton2, RadioButton3 };
                foreach (var radioButton in radioButtons)
                {
                    if (radioButton.Name.Contains(question.CorrectAnswer.ToString()))
                    {
                        elementOfRadioButton = radioButtons.IndexOf(radioButton);
                    }
                }

            }
        }
    }
}
