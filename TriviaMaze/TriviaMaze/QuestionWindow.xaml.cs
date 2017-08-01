// Team //noComment
//
// Matt Kerr
// Mary Floyd
// Tim Unger
//
// CSCD350
// Spring 2015

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
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        private Question question;
        private GameWindow game;
        private string type;
        private string answer;
        public bool set_answer;
        private bool result;

        public QuestionWindow(Question question, GameWindow game)
        {
            this.question = question;
            this.game = game;
            this.type = question.GetQuestionType();
            this.answer = question.GetAnswer();
            this.set_answer = false;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            btn_answer1.Background = Brushes.White;
            btn_answer2.Background = Brushes.White;
            btn_answer3.Background = Brushes.White;
            btn_answer4.Background = Brushes.White;
            icon_entertainment.Visibility = Visibility.Hidden;
            icon_history.Visibility = Visibility.Hidden;
            icon_science.Visibility = Visibility.Hidden;
            icon_sports.Visibility = Visibility.Hidden;
            SetQuestion();
            textBx_shortAnswer.Visibility = Visibility.Hidden;
            btn_submit.Visibility = Visibility.Hidden;

            if (type == "MC")
            {
                MultipleChoice();
            }
            else if (type == "TF")
            {
                TrueFalse();
            }
            else if (type == "SHORT")
            {
                ShortAnswer();
            }
        }

        private void MultipleChoice()
        {
            // Sets up the window for a multiple choice question

            btn_answer1.Content = question.GetOptionA();
            btn_answer2.Content = question.GetOptionB();
            btn_answer3.Content = question.GetOptionC();
            btn_answer4.Content = question.GetOptionD();
        }

        private void TrueFalse()
        {
            // Sets up the window for a true/false question

            btn_answer1.Content = question.GetOptionA();
            btn_answer2.Content = question.GetOptionB();
            btn_answer3.Visibility = Visibility.Hidden;
            btn_answer4.Visibility = Visibility.Hidden;
        }

        private void ShortAnswer()
        {
            // Sets up the window for a short answer question

            btn_answer1.Visibility = Visibility.Hidden;
            btn_answer2.Visibility = Visibility.Hidden;
            btn_answer3.Visibility = Visibility.Hidden;
            btn_answer4.Visibility = Visibility.Hidden;
            textBx_shortAnswer.Visibility = Visibility.Visible;
            btn_submit.Visibility = Visibility.Visible;
        }

        private void SetQuestion()
        {
            // Sets the Question to be used

            label_question.Text = question.GetQuestionString();
            if (question.GetCategory()=="Entertainment")
            {
                label_categorey.Background = Brushes.DeepPink;
                label_categorey.Content = "Entertainment";
                icon_entertainment.Visibility = Visibility.Visible;
            }
            if (question.GetCategory() == "Science")
            {
                label_categorey.Background = Brushes.LimeGreen;
                label_categorey.Content = "Science";
                icon_science.Visibility = Visibility.Visible;
            }
            if (question.GetCategory() == "Sports")
            {
                label_categorey.Background = Brushes.OrangeRed;
                label_categorey.Content = "Sports";
                icon_sports.Visibility = Visibility.Visible;
            }
            if (question.GetCategory() == "History")
            {
                label_categorey.Background = Brushes.DodgerBlue;
                label_categorey.Content = "History";
                icon_history.Visibility = Visibility.Visible;    
            }
        }
        
        private void btn_answer1_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the first answer button
            // Sets the user's answer and closes this window

            if (this.type == "MC")
            {
                game.SetUserAnswer("A");
            }
            else if (this.type == "TF")
            {
                game.SetUserAnswer("true");
            }

            this.set_answer = true;
            this.Hide();
            game.EndQuestion();
            
       
        }

        private void btn_answer2_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the second answer button
            // Sets the user's answer and closes this window

            if (this.type == "MC")
            {
                game.SetUserAnswer("B");
            }
            else if (this.type == "TF")
            {
                game.SetUserAnswer("false");
            }

            this.set_answer = true;
            this.Hide();
            game.EndQuestion();
            
        }

        private void btn_answer3_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the third answer button
            // Sets the user's answer and closes this window

            this.set_answer = true;
            game.SetUserAnswer("C");
            this.Hide();
            game.EndQuestion();
            
           
        }

        private void btn_answer4_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the fourth answer button
            // Sets the user's answer and closes this window

            this.set_answer = true;
            game.SetUserAnswer("D");
            this.Hide();
            game.EndQuestion();
            
            
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks the Submit button
            // for a short answer question
            // Sets the user's answer and closes this window

            this.set_answer = true;
            game.SetUserAnswer(textBx_shortAnswer.Text);
            this.Hide();
            game.EndQuestion();
        }

        private void textBx_shortAnswer_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Event handler for when the user types in the short answer box
            // Uses regular expressions to verify the input is valid
        }

        private void question_window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!set_answer)
            {
                // the user clicked the 'X' without answering the question
                game.SetUserAnswer("wrong");
                game.EndQuestion();
            }
        }





       
    }
}

