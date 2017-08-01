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
    /// Interaction logic for CorrectWindow.xaml
    /// </summary>
    public partial class CorrectWindow : Window
    {
        private GameWindow game;
        private Question question;
        public CorrectWindow(GameWindow game, Question question)
        {
            this.game = game;
            this.question = question;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Start();
        }

        private void Start()
        {
            // Initializes the window

            btn_continue.Background = Brushes.White;
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
            // Event handler for when the user clicks the Continue button

            this.Close();
            game.Show();
        }

        private void window_correct_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.Show();
        }
    }
}
