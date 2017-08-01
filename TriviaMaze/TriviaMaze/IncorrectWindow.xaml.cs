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
    /// Interaction logic for IncorrectWindow.xaml
    /// </summary>
    public partial class IncorrectWindow : Window
    {
        private GameWindow game;
        private Question question;
        public IncorrectWindow(string answer, GameWindow game, Question question)
        {
            this.question = question;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.game = game;
            label_answer.Content = "The correct answer is: "+answer;
            Start();
        }

        private void Start()
        {
            // Initializes the info displayed by this window

            label_question.Text = question.GetQuestionString();
            icon_entertainment.Visibility = Visibility.Hidden;
            icon_history.Visibility = Visibility.Hidden;
            icon_science.Visibility = Visibility.Hidden;
            icon_sports.Visibility = Visibility.Hidden;
            if (question.GetCategory() == "Entertainment")
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

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {
            // Event handler for when the user clicks Continue
            // Returns the user to the GameWindow

            this.Close();
            game.Show();
        }

        private void window_incorrect_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.Show();
        }
    }
}
