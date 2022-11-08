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

    public partial class CreateView : UserControl
    {
        QuizManager _quizManager = new QuizManager();
        private int CorrectAnswer { get; set; }

        public Quiz QuizToSave { get; set; }

        private bool radioButtonisChecked = false;

        private bool quizHasTitle = false;
        public CreateView()
        {
            InitializeComponent();
        }

        public void EmptyTextFields()
        {
            Question.Text = String.Empty;
            Answer1.Text = String.Empty;
            Answer2.Text = String.Empty;
            Answer3.Text = String.Empty;
            Answer4.Text = String.Empty;
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (quizHasTitle && Question.Text != "" && radioButtonisChecked)
            {
                QuizToSave.AddQuestion(Question.Text, CorrectAnswer, Answer1.Text, Answer2.Text, Answer3.Text, Answer4.Text);
                EmptyTextFields();
            }
            if (Title.Text != "" && quizHasTitle == false)
            {
                var quizWithTitle = new Quiz(Title.Text);
                QuizToSave = quizWithTitle;
                quizHasTitle = true;
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonisChecked = true;
            RadioButton radioButton = e.Source as RadioButton;

            foreach (var c in radioButton.Name)
            {
                if (char.IsDigit(c))
                {
                    CorrectAnswer = c-48;
                }
            }
        }

        private void FinaliseAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            _quizManager.CurrentQuiz = QuizToSave;
            _quizManager.SaveQuiz();
            Title.Text = String.Empty;
            EmptyTextFields();
        }
    }
}
